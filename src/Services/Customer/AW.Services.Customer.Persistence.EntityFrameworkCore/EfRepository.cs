using Ardalis.Specification.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.Customer.Persistence.EntityFrameworkCore
{
    public class EfRepository<T> : RepositoryBase<T> where T : class
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