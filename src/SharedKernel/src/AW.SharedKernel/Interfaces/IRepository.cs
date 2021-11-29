using Ardalis.Specification;
using System;
using System.Data.Common;
using System.Threading.Tasks;

namespace AW.SharedKernel.Interfaces
{
    public interface IRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot
    {
        //DbConnection DbConnection { get; }
        //DbTransaction CurrentTransaction { get; }
        //Guid CurrentTransactionId { get; }
        //bool HasActiveTransaction { get; }
        IUnitOfWork UnitOfWork { get; }

        //Task Execute(Func<Task> operation);
        //Task<DbTransaction> BeginTransactionAsync();
        //void UseTransaction(DbTransaction transaction);
        //Task CommitTransactionAsync(DbTransaction transaction);
    }
}