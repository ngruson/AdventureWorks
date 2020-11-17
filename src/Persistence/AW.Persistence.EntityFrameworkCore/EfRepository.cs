using Ardalis.Specification.EntityFrameworkCore;

namespace AW.Persistence.EntityFrameworkCore
{
    public class EfRepository<T> : RepositoryBase<T> where T : class
    {
        public EfRepository(AWContext dbContext) : base(dbContext)
        {

        }
    }
}