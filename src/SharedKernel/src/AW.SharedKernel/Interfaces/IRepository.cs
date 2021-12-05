using Ardalis.Specification;

namespace AW.SharedKernel.Interfaces
{
    public interface IRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}