using Ardalis.GuardClauses;
using AutoMapper;
using AW.Services.Customer.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.Customer.Core.Handlers.UpdateStoreCustomerContact
{
    public class UpdateStoreCustomerContactCommandHandler : IRequestHandler<UpdateStoreCustomerContactCommand, Unit>
    {
        private readonly ILogger<UpdateStoreCustomerContactCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<Entities.StoreCustomer> _storeRepository;

        public UpdateStoreCustomerContactCommandHandler(
            ILogger<UpdateStoreCustomerContactCommandHandler> logger,
            IMapper mapper,
            IRepository<Entities.StoreCustomer> storeRepository
        ) => (_logger, _mapper, _storeRepository) = (logger, mapper, storeRepository);
        
        public async Task<Unit> Handle(UpdateStoreCustomerContactCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle called");
            _logger.LogInformation("Getting customer from database");

            var storeCustomer = await _storeRepository.SingleOrDefaultAsync(
                new GetStoreCustomerSpecification(request.AccountNumber),
                cancellationToken
            );
            Guard.Against.Null(storeCustomer, _logger);

            var contact = storeCustomer.Contacts.FirstOrDefault(c =>
                c.ContactType == request.CustomerContact.ContactType
            );
            Guard.Against.Null(contact, _logger);

            _logger.LogInformation("Updating contact");
            _mapper.Map(request.CustomerContact, contact);

            _logger.LogInformation("Saving customer to database");
            await _storeRepository.UpdateAsync(storeCustomer, cancellationToken);

            return Unit.Value;
        }
    }
}