using Ardalis.GuardClauses;
using AW.Services.Customer.Core.GuardClauses;
using AW.Services.Customer.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.Services.Customer.Core.Handlers.DeleteStoreCustomerContact
{
    public class DeleteStoreCustomerContactCommandHandler : IRequestHandler<DeleteStoreCustomerContactCommand, Unit>
    {
        private readonly ILogger<DeleteStoreCustomerContactCommandHandler> _logger;
        private readonly IRepository<Entities.StoreCustomer> _repository;

        public DeleteStoreCustomerContactCommandHandler(
            ILogger<DeleteStoreCustomerContactCommandHandler> logger,
            IRepository<Entities.StoreCustomer> storeCustomerRepository
        ) => (_logger, _repository) = (logger, storeCustomerRepository);
        
        public async Task<Unit> Handle(DeleteStoreCustomerContactCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle called");
            _logger.LogInformation("Getting customer from database");

            var storeCustomer = await _repository.SingleOrDefaultAsync(
                new GetStoreCustomerSpecification(request.AccountNumber!),
                cancellationToken
            );
            Guard.Against.CustomerNull(storeCustomer, request.AccountNumber!, _logger);

            _logger.LogInformation("Removing phone from contact");
            var contact = storeCustomer!.Contacts.FirstOrDefault(
                c => c.ContactType == request.CustomerContact!.ContactType &&
                    c.ContactPerson!.Name == request.CustomerContact.ContactPerson!.Name
            );
            Guard.Against.StoreContactNull(contact,
                request.AccountNumber!,
                request.CustomerContact!.ContactPerson!.Name!.FullName,
                request.CustomerContact.ContactType!,
                _logger
            );

            storeCustomer.RemoveContact(contact!);

            _logger.LogInformation("Updating customer to database");
            await _repository.UpdateAsync(storeCustomer, cancellationToken);
            return Unit.Value;
        }
    }
}