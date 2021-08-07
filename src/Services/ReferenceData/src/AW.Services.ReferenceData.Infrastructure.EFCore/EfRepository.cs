using Ardalis.Specification.EntityFrameworkCore;
using AW.Services.SharedKernel.EFCore;
using AW.SharedKernel.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.ReferenceData.Infrastructure.EFCore
{
    public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
    {
        private readonly AWContext dbContext;

        public EfRepository(AWContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public override async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            dbContext.SetModified(entity);
            await SaveChangesAsync();
        }
    }
}