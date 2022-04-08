using Ardalis.GuardClauses;
using Ardalis.SmartEnum;
using AW.Services.Infrastructure;
using AW.SharedKernel.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SmartEnum.EFCore;
using System;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.SharedKernel.EFCore
{
    public class AWContext : DbContext, IDbContext, IUnitOfWork
    {
        private readonly Assembly configurationsAssembly;
        private readonly IMediator mediator;
        private IDbContextTransaction currentTransaction;

        public AWContext() { }

        public AWContext(
            DbContextOptions<AWContext> options,
            IMediator mediator
        ) : base(options)
        {
            this.mediator = mediator;
        }

        public AWContext(
            DbContextOptions<AWContext> options,             
            IMediator mediator,
            Assembly configurationsAssembly
        ) : base(options)
        {
            this.configurationsAssembly = configurationsAssembly;
            this.mediator = mediator;
        }

        public DbTransaction CurrentTransaction => currentTransaction.GetDbTransaction();
        public Guid CurrentTransactionId => currentTransaction.TransactionId;

        public bool HasActiveTransaction => currentTransaction != null;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (configurationsAssembly != null)
            {
                modelBuilder.ApplyConfigurationsFromAssembly(configurationsAssembly);
                modelBuilder.ConfigureSmartEnum();

                //foreach (var entityType in modelBuilder.Model.GetEntityTypes())
                //{
                //    var properties = entityType.ClrType.GetProperties()
                //        .Where(p => IsDerived(p.PropertyType, typeof(SmartEnum<,>)));

                //    foreach (var property in properties)
                //    {
                //        var keyType = GetValueType(property.PropertyType, typeof(SmartEnum<,>));

                //        //var converterType = typeof(SmartEnumConverter<,>).MakeGenericType(property.PropertyType, keyType);

                //        //var converter = (ValueConverter)Activator.CreateInstance(converterType);

                //        //modelBuilder.Entity(entityType.Name).Property(property.Name).HasConversion(converter);
                //    }
                //}
            }
        }

        public static bool IsDerived(Type objectType, Type mainType)
        {
            Type currentType = objectType.BaseType;

            if (currentType == null)
            {
                return false;
            }

            while (currentType != typeof(object))
            {
                if (currentType.IsGenericType && currentType.GetGenericTypeDefinition() == mainType)
                    return true;

                currentType = currentType.BaseType;
            }

            return false;
        }

        private static Type GetValueType(Type objectType, Type mainType)
        {
            Type currentType = objectType.BaseType;

            if (currentType == null)
            {
                return null;
            }

            while (currentType != typeof(object))
            {
                if (currentType.IsGenericType && currentType.GetGenericTypeDefinition() == mainType)
                    return currentType.GenericTypeArguments[1];

                currentType = currentType.BaseType;
            }

            return null;
        }

        public virtual void SetModified(object entity)
        {
            Entry(entity).State = EntityState.Modified;
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            await mediator.DispatchDomainEventsAsync(this);

            // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
            // performed through the DbContext will be committed
            await base.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task Execute(Func<Task> operation)
        {
            var strategy = Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(operation);
        }

        public async Task<DbTransaction> BeginTransactionAsync()
        {
            if (currentTransaction != null) return null;
            currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            return CurrentTransaction;
        }

        public async Task CommitTransactionAsync(DbTransaction transaction)
        {
            Guard.Against.Null(transaction, nameof(transaction));

            if (transaction != CurrentTransaction)
                throw new InvalidOperationException($"Transaction {CurrentTransactionId} is not current");

            try
            {
                await SaveChangesAsync();
                transaction.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (currentTransaction != null)
                {
                    currentTransaction.Dispose();
                    currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                currentTransaction?.Rollback();
            }
            finally
            {
                if (currentTransaction != null)
                {
                    currentTransaction.Dispose();
                    currentTransaction = null;
                }
            }
        }        
    }
}