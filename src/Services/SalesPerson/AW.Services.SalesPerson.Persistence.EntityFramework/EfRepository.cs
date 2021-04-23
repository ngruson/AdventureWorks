using Ardalis.Specification;
using Ardalis.Specification.EntityFramework;
using System.Threading.Tasks;

namespace AW.Services.SalesPerson.Persistence.EntityFramework
{
    public class EfRepository<T> : RepositoryBase<T> where T : class
    {
        public EfRepository(AWContext context) : base(context) { }
        public EfRepository(AWContext context, ISpecificationEvaluator<T> specificationEvaluator)
            : base(context, specificationEvaluator) { }

        public override async Task UpdateAsync(T entity)
        {
            ((AWContext)DbContext).SetModified(entity);
            await SaveChangesAsync();
        }
    }
}