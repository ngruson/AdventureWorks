using Ardalis.GuardClauses;
using AW.SharedKernel.Interfaces;
using MediatR;
using System;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.SharedKernel.EF6
{
    public partial class AWContext : DbContext, IDbContext, IUnitOfWork
    {
        private readonly Assembly configurationAssembly;
        private readonly IMediator mediator;
        private DbTransaction currentTransaction;
        private Guid currentTransactionId;

        public AWContext()
            : base("AWContext")
        {
        }        

        public AWContext(DbConnection existingConnection, bool contextOwnsConnection, Assembly configurationAssembly)
            : base(existingConnection, contextOwnsConnection)
        {
            this.configurationAssembly = configurationAssembly;
        }

        public DbTransaction CurrentTransaction => currentTransaction;
        public Guid CurrentTransactionId => currentTransactionId;

        public bool HasActiveTransaction => currentTransaction != null;

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
            await operation.Invoke();
        }

        public Task<DbTransaction> BeginTransactionAsync()
        {
            if (currentTransaction != null) 
                return Task.FromResult<DbTransaction>(null);

            var transaction = Database.BeginTransaction(IsolationLevel.ReadCommitted);
            currentTransaction = transaction.UnderlyingTransaction;
            currentTransactionId = Guid.NewGuid();

            return Task.FromResult(currentTransaction);
        }

        public async Task CommitTransactionAsync(DbTransaction transaction)
        {
            Guard.Against.Null(transaction, nameof(transaction));

            if (transaction != currentTransaction)
                throw new InvalidOperationException($"Transaction {currentTransactionId} is not current");

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

        public virtual void SetModified(object entity)
        {
            Entry(entity).State = EntityState.Modified;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            if (configurationAssembly != null)
            {
                var typesToRegister = configurationAssembly.GetTypes()
                  .Where(type => !string.IsNullOrEmpty(type.Namespace))
                  .Where(type => type.BaseType != null && type.BaseType.IsGenericType
                       && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));

                foreach (var type in typesToRegister)
                {
                    dynamic configurationInstance = Activator.CreateInstance(type);
                    modelBuilder.Configurations.Add(configurationInstance);
                }
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}