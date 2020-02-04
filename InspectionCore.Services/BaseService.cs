using InspectionCore.Common;
using InspectionCore.Reposiotories;
using InspectionCore.Services.Validations;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InspectionCore.Services
{
	public class BaseService : IBaseService
	{
		protected readonly RequestScope ScopeContext;

		public BaseService(RequestScope scopeContext)
		{
			this.ScopeContext = scopeContext;
		}

		public virtual void Dispose()
		{
		}
	}

	public class BaseService<TEntity, TKey> : BaseService, IBaseService<TEntity, TKey>
		where TEntity : class, IBaseEntity<TKey>, new()
	{
		private readonly List<IPropertyValidation<TEntity>> _propertyValidations;
		protected IEFRepository<TEntity, TKey> Repository { get; }
		protected readonly IEnumerable<Expression<Func<TEntity, IEnumerable<IBaseEntity>>>> _childExpressions;

		public BaseService(RequestScope scopeContext, IEFRepository<TEntity, TKey> repository)
			: base(scopeContext)
		{
			this.Repository = repository;

			if (_propertyValidations == null)
			{
				_propertyValidations = new List<IPropertyValidation<TEntity>>();
				Validation();
			}
		}

		public BaseService(RequestScope scopeContext, IEFRepository<TEntity, TKey> repository, params Expression<Func<TEntity, IEnumerable<IBaseEntity>>>[] childExpressions)
			: this(scopeContext, repository)
		{
			this._childExpressions = childExpressions;
		}


		protected virtual void Validation()
		{

		}

		protected PropertyValidation<TEntity, TProperty> Validate<TProperty>(Expression<Func<TEntity, TProperty>> property, string caption = null)
		{
			var result = new PropertyValidation<TEntity, TProperty>(ScopeContext.ServiceProvider, this.Repository, property, ValidationType.None, caption ?? (property.Body as MemberExpression).Member.Name);
			_propertyValidations.Add(result);
			return result;
		}

		private async Task Validate(TEntity entity)
		{
			List<string> errors = new List<string>();
			await Task.WhenAll(_propertyValidations
				.Select(async validator =>
				{
					var validateResult = await validator.Validate(entity);

					if (!validateResult.valid)
					{
						errors.Add(validateResult.error);
					}
				}));

			if (errors.Count > 0)
			{
				throw new ServiceException(errors.ToArray());
			}
		}

		protected virtual Task WhileInserting(IEnumerable<TEntity> entities) { return Task.FromResult(false); }
		protected virtual Task OnInserted(IEnumerable<TEntity> entities) { return Task.FromResult(false); }
		protected virtual Task WhileUpdating(IEnumerable<TEntity> entities) { return Task.FromResult(false); }
		protected virtual Task OnUpdated(IEnumerable<TEntity> entities) { return Task.FromResult(false); }
		protected virtual Task WhileDeleting(IEnumerable<TEntity> entities) { return Task.FromResult(false); }
		protected virtual Task OnDeleted(IEnumerable<TEntity> entities) { return Task.FromResult(false); }

		#region Get
		public async Task<Dictionary<TKey, TEntity>> Get()
		{
			return await this.Repository.Get();
		}

		public async Task<Dictionary<TKey, TEntity>> Get(params Expression<Func<TEntity, bool>>[] predicates)
		{
			return await Repository.Get(predicates);
		}

		public async Task<TEntity> GetOne(params Expression<Func<TEntity, bool>>[] predicates)
		{
			return await Repository.GetOne(predicates);
		}

		#endregion

		public async Task<(Dictionary<TKey, TEntity> Entities, bool Success)> Insert(IEnumerable<TEntity> entities)
		{
			ConcurrentBag<TEntity> newEntities = new ConcurrentBag<TEntity>();
			await Task.WhenAll(
				entities.Select(async entity =>
				{
					var newEntity = new TEntity();
					this.Map(entity, newEntity);
					await this.Validate(newEntity);
					newEntities.Add(newEntity);
				}));

			await this.WhileInserting(newEntities);
			await Repository.Insert(newEntities);
			var result = await Repository.SaveChanges() ? (newEntities.ToDictionary(o => o.Id), true) : (null, false);
			await OnInserted(newEntities);
			return result;
		}

		public async Task<(Dictionary<TKey, TEntity> Entities, bool Success)> BulkInsert(IEnumerable<TEntity> entities)
		{
			ConcurrentBag<TEntity> newEntities = new ConcurrentBag<TEntity>();
			await Task.WhenAll(
				entities.Select(async entity =>
				{
					var newEntity = new TEntity();
					this.Map(entity, newEntity);
					newEntities.Add(newEntity);
				}));

			await this.WhileInserting(newEntities);
			await Repository.BulkInsert(newEntities);
			var result = (newEntities.ToDictionary(o => o.Id), true);
			await OnInserted(newEntities);
			return result;
		}

		public async Task<(TEntity Entity, bool Success)> Update(TKey id, TEntity entity)
		{
			var dbEntity = await this.Repository.GetOne(id);
			this.Map(entity, dbEntity);

			await this.WhileUpdating(new[] { dbEntity });
			var result = Repository.Update(id, dbEntity) & await Repository.SaveChanges() ? (await this.Repository.GetOne(id), true) : (null, false);
			await this.OnUpdated(new[] { dbEntity });
			return result;
		}

		public async Task<bool> Delete(TKey id)
		{
			var dbEntity = await this.Repository.GetOne(id);
			await this.WhileDeleting(new[] { dbEntity });
			var result = await this.Repository.Delete(id, dbEntity) & await this.Repository.SaveChanges();
			await this.OnDeleted(new[] { dbEntity });

			return result;
		}

		protected void Map(TEntity source, TEntity dest)
		{
			ScopeContext.Mapper.Map(source, dest);

			if (this._childExpressions is null) return;
			foreach (var child in this._childExpressions)
			{
				var func = child.Compile();
				var destLists = func(dest);
				var sourceLists = func(source);
				var toDelete = new List<IBaseEntity>();
				// Delete
				foreach (IBaseEntity destChild in destLists)
				{
					if (!sourceLists.OfType<IBaseEntity>().Any(o => o.Id.Equals(destChild.Id)))
					{
						// Remove
						destChild.IsDeleted = true;
					}
				}

				// Add/Modify
				foreach (IBaseEntity sourceChild in sourceLists)
				{
					IBaseEntity destChild;
					destChild = destLists.OfType<IBaseEntity>().FirstOrDefault(o => !o.IsNew && o.Id.Equals(sourceChild.Id));

					if (destChild == null)
					{
						destChild = Activator.CreateInstance(destLists.GetType().GetGenericArguments()[0]) as IBaseEntity;
						destLists.GetType().GetMethod("Add").Invoke(destLists, new[] { destChild });
					}

					ScopeContext.Mapper.Map(sourceChild, destChild);
				}
			}
		}

		public override void Dispose()
		{
			base.Dispose();
		}
	}

	public interface IBaseService : IDisposable
	{

	}

	public interface IBaseService<TEntity, TKey> : IBaseService
		where TEntity : class, IBaseEntity<TKey>, new()
	{
		#region Get
		Task<Dictionary<TKey, TEntity>> Get();
		Task<Dictionary<TKey, TEntity>> Get(params Expression<Func<TEntity, bool>>[] predicates);
		Task<TEntity> GetOne(params Expression<Func<TEntity, bool>>[] predicates);

		#endregion

		Task<(Dictionary<TKey, TEntity> Entities, bool Success)> Insert(IEnumerable<TEntity> entities);
		Task<(Dictionary<TKey, TEntity> Entities, bool Success)> BulkInsert(IEnumerable<TEntity> entities);
		Task<(TEntity Entity, bool Success)> Update(TKey id, TEntity entity);
		Task<bool> Delete(TKey id);
	}
}
