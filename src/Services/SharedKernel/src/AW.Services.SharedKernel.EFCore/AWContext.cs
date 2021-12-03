using Ardalis.GuardClauses;
using AW.SharedKernel.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
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
            Assembly configurationsAssembly,
            IMediator mediator
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
                modelBuilder.ApplyConfigurationsFromAssembly(configurationsAssembly);
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