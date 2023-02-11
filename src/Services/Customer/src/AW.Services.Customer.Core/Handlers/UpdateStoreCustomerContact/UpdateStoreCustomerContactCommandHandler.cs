using Ardalis.GuardClauses;
using AutoMapper;
using AW.Services.Customer.Core.GuardClauses;
using AW.Services.Customer.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.Services.Customer.Core.Handlers.UpdateStoreCustomerContact
{
    public class UpdateStoreCustomerContactCommandHandler : IRequestHandler<UpdateStoreCustomerContactCommand, Unit>
    {
        private readonly ILogger<UpdateStoreCustomerContactCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<Entities.StoreCustomer> _repository;

        public UpdateStoreCustomerContactCommandHandler(
            ILogger<UpdateStoreCustomerContactCommandHandler> logger,
            IMapper mapper,
            IRepository<Entities.StoreCustomer> repository
        ) => (_logger, _mapper, _repository) = (logger, mapper, repository);
        
        public async Task<Unit> Handle(UpdateStoreCustomerContactCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle called");
            _logger.LogInformation("Getting customer from database");

            var storeCustomer = await _repository.SingleOrDefaultAsync(
                new GetStoreCustomerSpecification(request.AccountNumber),
                cancellationToken
            );
            Guard.Against.CustomerNull(storeCustomer, request.AccountNumber, _logger);

            var contact = storeCustomer?.Contacts.FirstOrDefault(c =>
                c.ContactType == request.CustomerContact.ContactType
            );
            Guard.Against.StoreContactNull(
                contact,
                request.AccountNumber,
                request.CustomerContact.ContactPerson?.Name!.FullName,
                request.CustomerContact.ContactType!,
                _logger
            );

            _logger.LogInformation("Updating contact");
            _mapper.Map(request.CustomerContact, contact);

            _logger.LogInformation("Saving customer to database");
            await _repository.UpdateAsync(storeCustomer!, cancellationToken);

            return Unit.Value;
        }
    }
}