using Ardalis.Specification.EntityFrameworkCore;
using AW.Services.SharedKernel.EFCore;
using AW.SharedKernel.Interfaces;

namespace AW.Services.Product.Infrastructure.EFCore
{
    public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
    {
        public EfRepository(AWContext dbContext) : base(dbContext)
        {

        }
    }
}