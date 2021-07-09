using Ardalis.Specification.EntityFrameworkCore;

namespace AW.Services.Product.Infrastructure.EFCore
{
    public class EfRepository<T> : RepositoryBase<T> where T : class
    {
        public EfRepository(AWContext dbContext) : base(dbContext)
        {

        }
    }
}