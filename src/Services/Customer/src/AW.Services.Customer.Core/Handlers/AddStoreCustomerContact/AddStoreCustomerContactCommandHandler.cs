using Ardalis.GuardClauses;
using AutoMapper;
using AW.Services.Customer.Core.GuardClauses;
using AW.Services.Customer.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.Customer.Core.Handlers.AddStoreCustomerContact
{
    public class AddStoreCustomerContactCommandHandler : IRequestHandler<AddStoreCustomerContactCommand, Unit>
    {
        private readonly ILogger<AddStoreCustomerContactCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<Entities.StoreCustomer> _repository;

        public AddStoreCustomerContactCommandHandler(
            ILogger<AddStoreCustomerContactCommandHandler> logger,
            IMapper mapper,
            IRepository<Entities.StoreCustomer> repository
        ) => (_logger, _mapper, _repository) = (logger, mapper, repository);
        
        public async Task<Unit> Handle(AddStoreCustomerContactCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle called");
            _logger.LogInformation("Getting customer from database");

            var storeCustomer = await _repository.SingleOrDefaultAsync(
                new GetStoreCustomerSpecification(request.AccountNumber),
                cancellationToken
            );
            Guard.Against.CustomerNull(storeCustomer, request.AccountNumber, _logger);

            _logger.LogInformation("Adding contact to store");
            var contact = _mapper.Map<Entities.StoreCustomerContact>(request.CustomerContact);
            storeCustomer.AddContact(contact);

            _logger.LogInformation("Saving customer to database");
            await _repository.UpdateAsync(storeCustomer, cancellationToken);

            return Unit.Value;
        }
    }
}