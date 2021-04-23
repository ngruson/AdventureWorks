using Ardalis.GuardClauses;
using Ardalis.Specification;
using AutoMapper;
using AW.Services.Customer.Application.Specifications;
using AW.Services.Customer.Application.UpdateStoreCustomerContact;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.Customer.Application.AddStoreCustomerContact
{
    public class UpdateStoreCustomerContactCommandHandler : IRequestHandler<UpdateStoreCustomerContactCommand, Unit>
    {
        private readonly ILogger<UpdateStoreCustomerContactCommandHandler> logger;
        private readonly IMapper mapper;
        private readonly IRepositoryBase<Domain.StoreCustomer> storeRepository;

        public UpdateStoreCustomerContactCommandHandler(
            ILogger<UpdateStoreCustomerContactCommandHandler> logger,
            IMapper mapper,
            IRepositoryBase<Domain.StoreCustomer> storeRepository
        ) => (this.logger, this.mapper, this.storeRepository) = (logger, mapper, storeRepository);
        
        public async Task<Unit> Handle(UpdateStoreCustomerContactCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handle called");
            logger.LogInformation("Getting customer from database");

            var store = await storeRepository.GetBySpecAsync(
                new GetStoreCustomerSpecification(request.AccountNumber)
            );
            Guard.Against.Null(store, nameof(store));

            var contact = store.Contacts.FirstOrDefault(c =>
                c.ContactType == request.CustomerContact.ContactType
            );
            Guard.Against.Null(contact, nameof(contact));

            logger.LogInformation("Updating contact");
            mapper.Map(request.CustomerContact, contact);

            logger.LogInformation("Saving customer to database");
            await storeRepository.UpdateAsync(store);

            return Unit.Value;
        }
    }
}