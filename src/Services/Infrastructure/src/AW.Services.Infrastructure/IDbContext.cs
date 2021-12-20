using System;
using System.Data.Common;
using System.Threading.Tasks;

namespace AW.Services.Infrastructure
{
    public interface IDbContext
    {
        bool HasActiveTransaction { get; }
        DbTransaction CurrentTransaction { get; }
        Guid CurrentTransactionId { get; }
        Task Execute(Func<Task> operation);
        Task<DbTransaction> BeginTransactionAsync();
        Task CommitTransactionAsync(DbTransaction transaction);
    }
}