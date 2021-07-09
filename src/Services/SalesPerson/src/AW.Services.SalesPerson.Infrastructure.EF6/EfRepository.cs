using Ardalis.Specification.EntityFramework;
using AW.SharedKernel.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.SalesPerson.Infrastructure.EF6
{
    public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
    {
        private readonly AWContext context;

        public EfRepository(AWContext context) : base(context)
        {
            this.context = context;
        }

        public override async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            context.SetModified(entity);
            await SaveChangesAsync();
        }
    }
}