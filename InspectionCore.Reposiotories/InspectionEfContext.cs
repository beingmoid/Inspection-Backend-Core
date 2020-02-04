using InspectionCore.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace InspectionCore.Reposiotories
{
	public abstract class InspectionEfContext : DbContext
	{
		public readonly IRequestInfo RequestInfo;
		private readonly string _connectionString;
		private ModelBuilder _modelBuilder;

		public InspectionEfContext(IRequestInfo requestInfo, string connectionString)
		{
			RequestInfo = requestInfo;
			_connectionString = connectionString;
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			_modelBuilder = modelBuilder;

			InitializeEntities();
			SeedStaticData(modelBuilder);
			SeedTestingData(modelBuilder);
		}
		

		protected abstract void InitializeEntities();

		protected abstract void SeedStaticData(ModelBuilder modelBuilder);

		protected abstract void SeedTestingData(ModelBuilder modelBuilder);

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer(_connectionString);

		protected EntityTypeBuilder<TEntity> InitializeEntity<TEntity>()
			where TEntity : class, IBaseEntity
		{
			var entityTypeBuilder = _modelBuilder.Entity<TEntity>();

			entityTypeBuilder
				.HasQueryFilter(o => !o.IsDeleted);

			entityTypeBuilder
			   .Property(o => o.TenantId)
			   .HasMaxLength(100)
			   .HasValueGenerator<TenantIdGenerator>();

			entityTypeBuilder
				.Property(o => o.CompanyId)
				.HasMaxLength(100);

			entityTypeBuilder
				.Property(o => o.CreateUserId)
				.HasMaxLength(100)
				.HasValueGenerator<UserIdGenerator>();

			entityTypeBuilder
				.Property(o => o.CreateTime)
				.HasValueGenerator<CurrentTimeGenerator>();

			//entityTypeBuilder
			//	.Property(o => o.Timestamp)
			//	.IsRowVersion();

			entityTypeBuilder
				.Property(o => o.Timestamp)
				.IsRowVersion();

			return entityTypeBuilder;
		}

		protected void CreateRelation<TEntity, TRelated>(Expression<Func<TEntity, IEnumerable<TRelated>>> navigationExpressionMany
			, Expression<Func<TRelated, TEntity>> navigationExpressionOne, Expression<Func<TRelated, dynamic>> foreignKeyExpression)
			where TEntity : class, IBaseEntity
			where TRelated : class, IBaseEntity
		{
			_modelBuilder.Entity<TEntity>()
				.HasMany(navigationExpressionMany)
				.WithOne(navigationExpressionOne)
				.HasForeignKey(foreignKeyExpression)
				.OnDelete(DeleteBehavior.Restrict);
		}

		protected void CreateRelation<TEntity, TRelated>(Expression<Func<TEntity, TRelated>> navigationExpressionThis
			, Expression<Func<TRelated, TEntity>> navigationExpressionOne, Expression<Func<TRelated, dynamic>> foreignKeyExpression)
			where TEntity : class, IBaseEntity
			where TRelated : class, IBaseEntity
		{
			_modelBuilder.Entity<TEntity>()
				.HasOne(navigationExpressionThis)
				.WithOne(navigationExpressionOne)
				.HasForeignKey(foreignKeyExpression)
				.OnDelete(DeleteBehavior.Restrict);
		}
		

		public void SeedData<TEntity>(params TEntity[] data)
			where TEntity : class
		{
			_modelBuilder.Entity<TEntity>().HasData(data);
		}
	}
}
