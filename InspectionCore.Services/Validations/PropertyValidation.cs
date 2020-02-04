using InspectionCore.Common;
using InspectionCore.Reposiotories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InspectionCore.Services.Validations
{
	public class PropertyValidation<TEntity, TProperty> : IPropertyValidation<TEntity>
		 where TEntity : class, IBaseEntity, new()
	{
		private IServiceProvider _serviceProvider;
		private IEFRepository<TEntity> _repository;
		private readonly Expression<Func<TEntity, TProperty>> _property;
		private readonly string _propertyName;
		private readonly Func<TEntity, TProperty> _propertyFunc;
		private readonly ValidationType _type;
		private readonly string _caption;
		private Func<TEntity, string> _errorExpression;
		private IEFRepository _foreignKeyRepository;
		private Expression<Func<TEntity, bool>> _validationExpression;
		private Func<TEntity, bool> _customExpression;
		private Func<TEntity, Task<bool>> _customExpressionAsync;
		private PropertyValidation<TEntity, TProperty> _nextPropertyValidation;
		internal PropertyValidation(IServiceProvider serviceProvider, IEFRepository<TEntity> repository, Expression<Func<TEntity, TProperty>> property, ValidationType validationType, string caption)
		{
			_serviceProvider = serviceProvider;
			_repository = repository;
			_property = property;
			_propertyName = (_property.Body as MemberExpression).Member.Name;
			_propertyFunc = property.Compile();
			_type = validationType;
			_caption = caption;
		}

		private PropertyValidation<TEntity, TProperty> NextValidation(ValidationType validationType) => _nextPropertyValidation = new PropertyValidation<TEntity, TProperty>(_serviceProvider, _repository, _property, validationType, _caption);

		public PropertyValidation<TEntity, TProperty> Mandatory(Expression<Func<TEntity, bool>> validationExpression = null, Func<TEntity, string> errorExpression = null)
		{
			NextValidation(ValidationType.Mandatory);
			_nextPropertyValidation._validationExpression = validationExpression;
			_nextPropertyValidation._errorExpression = errorExpression;
			return _nextPropertyValidation;
		}
		public PropertyValidation<TEntity, TProperty> Duplicate(Expression<Func<TEntity, bool>> validationExpression = null, Func<TEntity, string> errorExpression = null)
		{
			NextValidation(ValidationType.Duplicate);
			_nextPropertyValidation._errorExpression = errorExpression;
			_nextPropertyValidation._validationExpression = validationExpression;
			return _nextPropertyValidation;
		}
		public PropertyValidation<TEntity, TProperty> Custom(Func<TEntity, bool> expression, Func<TEntity, string> errorExpression)
		{
			NextValidation(ValidationType.Expression);
			_nextPropertyValidation._customExpression = expression;
			_nextPropertyValidation._errorExpression = errorExpression;
			return _nextPropertyValidation;
		}
		public PropertyValidation<TEntity, TProperty> Custom(Func<TEntity, Task<bool>> expression, Func<TEntity, string> errorExpression)
		{
			NextValidation(ValidationType.Expression);
			_nextPropertyValidation._customExpressionAsync = expression;
			_nextPropertyValidation._errorExpression = errorExpression;
			return _nextPropertyValidation;
		}

		public PropertyValidation<TEntity, TProperty> ForeignKey<TRepository>(Func<TEntity, string> errorExpression = null)
			where TRepository : IEFRepository
		{
			NextValidation(ValidationType.ForeignKey);
			_nextPropertyValidation._foreignKeyRepository = _serviceProvider.GetRequiredService<TRepository>();
			_nextPropertyValidation._errorExpression = errorExpression;
			return _nextPropertyValidation;
		}

		public async Task<(bool valid, string error)> Validate(TEntity entity)
		{
			(bool valid, string error) result = (true, string.Empty);
			var value = _propertyFunc(entity);

			switch (_type)
			{
				case ValidationType.Mandatory:
					var isEmpty = EqualityComparer<TProperty>.Default.Equals(value, default(TProperty));
					if (!isEmpty)
					{
						switch (value)
						{
							case int intValue when intValue == 0:
							case long longValue when longValue == 0:
							case float floatValue when floatValue == 0:
							case double doubleValue when doubleValue == 0:
							case string strValue when string.IsNullOrWhiteSpace(strValue):
								result = (false, _errorExpression?.Invoke(entity) ?? $"{_caption} is required.");
								break;
						}
					}
					else
					{
						result = (false, _errorExpression?.Invoke(entity) ?? $"{_caption} is required.");
					}
					break;
				case ValidationType.Duplicate:
					var parameter = Expression.Parameter(typeof(TEntity), typeof(TEntity).Name);
					var member = Expression.Property(parameter, _propertyName);
					var constant = Expression.Constant(value);
					var duplicateExpression = Expression.Lambda<Func<TEntity, bool>>(Expression.Equal(member, constant), parameter);

					var conditions = new[] { _validationExpression, duplicateExpression, null };

					if (_propertyName != nameof(entity.Id))
					{
						conditions[2] = o => o.Id != entity.Id;
					}

					if (await _repository.Any(conditions))
					{
						result = (false, _errorExpression?.Invoke(entity) ?? $"{_caption} can not be duplicate.");
					}
					break;
				case ValidationType.Expression:
					if (!_customExpression(entity))
					{
						result = (false, _errorExpression(entity));
					}
					break;
				case ValidationType.ExpressionAync:
					if (!await _customExpressionAsync(entity))
					{
						result = (false, _errorExpression(entity));
					}
					break;
				case ValidationType.ForeignKey:
					if (!await _foreignKeyRepository.Any(value))
					{
						result = (false, _errorExpression?.Invoke(entity) ?? $"Foreign key for {_caption} does not exist.");
					}
					break;
			}

			if (result.valid && _nextPropertyValidation != null)
			{
				result = await _nextPropertyValidation.Validate(entity);
			}

			return result;
		}

		~PropertyValidation()
		{

		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}
	}

	public interface IPropertyValidation<TEntity> : IDisposable
	{
		Task<(bool valid, string error)> Validate(TEntity entity);
	}

	enum ValidationType
	{
		None,
		Mandatory,
		Duplicate,
		Expression,
		ExpressionAync,
		ForeignKey,
	}
}
