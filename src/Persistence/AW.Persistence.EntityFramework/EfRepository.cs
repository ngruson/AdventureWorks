using Ardalis.Specification.EntityFramework;
using System.Threading.Tasks;

namespace AW.Persistence.EntityFramework
{
    public class EfRepository<T> : RepositoryBase<T> where T : class
    {
        public EfRepository(AWContext context) : base(context) { }
        
        public override async Task UpdateAsync(T entity)
        {
            ((AWContext)DbContext).SetModified(entity);
            await SaveChangesAsync();
        }
    }
}