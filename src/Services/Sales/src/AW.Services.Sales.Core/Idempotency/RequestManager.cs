using AW.Services.Sales.Core.Exceptions;
using AW.Services.SharedKernel.Interfaces;
using System;
using System.Threading.Tasks;

namespace AW.Services.Sales.Core.Idempotency
{
    public class RequestManager : IRequestManager
    {
        private readonly IRepository<ClientRequest> repository;

        public RequestManager(IRepository<ClientRequest> repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<bool> ExistAsync(Guid id)
        {
            var request = await repository.GetByIdAsync(id);
            return request != null;
        }

        public async Task CreateRequestForCommandAsync<T>(Guid id)
        {
            var exists = await ExistAsync(id);

            var request = exists ?
                throw new SalesDomainException($"Request with {id} already exists") :
                new ClientRequest()
                {
                    Id = id,
                    Name = typeof(T).Name,
                    Time = DateTime.UtcNow
                };

            await repository.AddAsync(request);

            await repository.SaveChangesAsync();
        }
    }
}