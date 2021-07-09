using Ardalis.GuardClauses;
using AW.Services.Customer.Core.Specifications;
using AW.SharedKernel.Extensions;
using AW.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.Customer.Core.Handlers.DeleteStoreCustomerContact
{
    public class DeleteStoreCustomerContactCommandHandler : IRequestHandler<DeleteStoreCustomerContactCommand, Unit>
    {
        private readonly ILogger<DeleteStoreCustomerContactCommandHandler> logger;
        private readonly IRepository<Entities.StoreCustomer> storeCustomerRepository;

        public DeleteStoreCustomerContactCommandHandler(
            ILogger<DeleteStoreCustomerContactCommandHandler> logger,
            IRepository<Entities.StoreCustomer> storeCustomerRepository
        ) => (this.logger, this.storeCustomerRepository) = (logger, storeCustomerRepository);
        
        public async Task<Unit> Handle(DeleteStoreCustomerContactCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handle called");
            logger.LogInformation("Getting customer from database");

            var storeCustomer = await storeCustomerRepository.GetBySpecAsync(
                new GetStoreCustomerSpecification(request.AccountNumber)
            );
            Guard.Against.Null(storeCustomer, nameof(storeCustomer), logger);

            logger.LogInformation("Removing phone from contact");
            var contact = storeCustomer.Contacts.FirstOrDefault(
                c => c.ContactType == request.CustomerContact.ContactType &&
                    c.ContactPerson.FirstName == request.CustomerContact.ContactPerson.FirstName &&
                    c.ContactPerson.MiddleName == request.CustomerContact.ContactPerson.MiddleName &&
                    c.ContactPerson.LastName == request.CustomerContact.ContactPerson.LastName
            );
            Guard.Against.Null(contact, nameof(contact), logger);

            storeCustomer.Contacts.Remove(contact);

            logger.LogInformation("Updating customer to database");
            await storeCustomerRepository.UpdateAsync(storeCustomer);
            return Unit.Value;
        }
    }
}