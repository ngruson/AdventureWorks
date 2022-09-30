using Ardalis.GuardClauses;
using AW.Services.Infrastructure;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SmartEnum.EFCore;
using System;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.SharedKernel.EFCore
{
    public class AWContext : DbContext, IDbContext, IUnitOfWork
    {
        private readonly Assembly _configurationsAssembly;
        private readonly IMediator _mediator;
        private IDbContextTransaction _currentTransaction;

        public AWContext() { }

        public AWContext(
            DbContextOptions<AWContext> options,
            IMediator mediator
        ) : base(options)
        {
            _mediator = mediator;
        }

        public AWContext(
            DbContextOptions<AWContext> options,             
            IMediator mediator,
            Assembly configurationsAssembly
        ) : base(options)
        {
            _configurationsAssembly = configurationsAssembly;
            _mediator = mediator;
        }

        public DbTransaction CurrentTransaction => _currentTransaction.GetDbTransaction();
        public Guid CurrentTransactionId => _currentTransaction.TransactionId;

        public bool HasActiveTransaction => _currentTransaction != null;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (_configurationsAssembly != null)
            {
                modelBuilder.ApplyConfigurationsFromAssembly(_configurationsAssembly);
                modelBuilder.ConfigureSmartEnum();
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

        public virtual void SetModified(object entity)
        {
            Entry(entity).State = EntityState.Modified;
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            await _mediator.DispatchDomainEventsAsync(this);

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
            if (_currentTransaction != null) return null;
            _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            return CurrentTransaction;
        }

        public async Task CommitTransactionAsync(DbTransaction transaction)
        {
            Guard.Against.Null(transaction, _logger);

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
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }        
    }
}