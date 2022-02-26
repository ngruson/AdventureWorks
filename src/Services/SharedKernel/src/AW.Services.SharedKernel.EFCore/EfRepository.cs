using Ardalis.Specification.EntityFrameworkCore;
using AW.SharedKernel.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.SharedKernel.EFCore
{
    public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
    {
        private readonly AWContext dbContext;
        public IUnitOfWork UnitOfWork => dbContext;

        public EfRepository(AWContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public override async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            dbContext.SetModified(entity);
            await SaveChangesAsync(cancellationToken);
        }
    }
}