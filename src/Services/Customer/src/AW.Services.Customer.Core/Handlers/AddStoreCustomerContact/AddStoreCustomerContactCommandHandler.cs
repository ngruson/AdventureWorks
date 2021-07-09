using Ardalis.GuardClauses;
using AutoMapper;
using AW.Services.Customer.Core.Specifications;
using AW.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.Customer.Core.Handlers.AddStoreCustomerContact
{
    public class AddStoreCustomerContactCommandHandler : IRequestHandler<AddStoreCustomerContactCommand, Unit>
    {
        private readonly ILogger<AddStoreCustomerContactCommandHandler> logger;
        private readonly IMapper mapper;
        private readonly IRepository<Entities.StoreCustomer> storeRepository;

        public AddStoreCustomerContactCommandHandler(
            ILogger<AddStoreCustomerContactCommandHandler> logger,
            IMapper mapper,
            IRepository<Entities.StoreCustomer> storeRepository
        ) => (this.logger, this.mapper, this.storeRepository) = (logger, mapper, storeRepository);
        
        public async Task<Unit> Handle(AddStoreCustomerContactCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handle called");
            logger.LogInformation("Getting customer from database");

            var storeCustomer = await storeRepository.GetBySpecAsync(
                new GetStoreCustomerSpecification(request.AccountNumber)
            );
            Guard.Against.Null(storeCustomer, nameof(storeCustomer));

            logger.LogInformation("Adding contact to store");
            var contact = mapper.Map<Entities.StoreCustomerContact>(request.CustomerContact);
            storeCustomer.Contacts.Add(contact);

            logger.LogInformation("Saving customer to database");
            await storeRepository.UpdateAsync(storeCustomer);

            return Unit.Value;
        }
    }
}