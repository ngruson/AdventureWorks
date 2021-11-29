using System;
using System.Threading.Tasks;

namespace AW.Services.SalesOrder.Core.Idempotency
{
    public interface IRequestManager
    {
        Task<bool> ExistAsync(Guid id);

        Task CreateRequestForCommandAsync<T>(Guid id);
    }
}