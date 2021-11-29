using Ardalis.Specification.EntityFramework6;
using AW.Services.SharedKernel.EF6;
using AW.SharedKernel.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.SalesOrder.Infrastructure.EF6
{
    public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
    {
        private readonly AWContext dbContext;

        public EfRepository(AWContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public IUnitOfWork UnitOfWork => dbContext;

        public override async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            dbContext.SetModified(entity);
            await SaveChangesAsync();
        }
    }
}