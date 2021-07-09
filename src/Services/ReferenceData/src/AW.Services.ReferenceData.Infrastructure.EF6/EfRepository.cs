using Ardalis.Specification.EntityFramework;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.ReferenceData.Infrastructure.EF6
{
    public class EfRepository<T> : RepositoryBase<T> where T : class
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