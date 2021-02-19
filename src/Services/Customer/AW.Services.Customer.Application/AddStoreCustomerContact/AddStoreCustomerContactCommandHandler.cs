using Ardalis.Specification;
using AutoMapper;
using AW.Services.Customer.Application.Specifications;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.Customer.Application.AddStoreCustomerContact
{
    public class AddStoreCustomerContactCommandHandler : IRequestHandler<AddStoreCustomerContactCommand, Unit>
    {
        private readonly ILogger<AddStoreCustomerContactCommandHandler> logger;
        private readonly IMapper mapper;
        private readonly IRepositoryBase<Domain.StoreCustomer> storeRepository;

        public AddStoreCustomerContactCommandHandler(
            ILogger<AddStoreCustomerContactCommandHandler> logger,
            IMapper mapper,
            IRepositoryBase<Domain.StoreCustomer> storeRepository
        ) => (this.logger, this.mapper, this.storeRepository) = (logger, mapper, storeRepository);
        
        public async Task<Unit> Handle(AddStoreCustomerContactCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handle called");
            logger.LogInformation("Getting customer from database");

            var store = await storeRepository.GetBySpecAsync(
                new GetStoreCustomerSpecification(request.AccountNumber)
            );

            logger.LogInformation("Adding contact to store");
            var contact = mapper.Map<Domain.StoreCustomerContact>(request.CustomerContact);
            store.Contacts.Add(contact);

            logger.LogInformation("Saving customer to database");
            await storeRepository.UpdateAsync(store);

            return Unit.Value;
        }
    }
}