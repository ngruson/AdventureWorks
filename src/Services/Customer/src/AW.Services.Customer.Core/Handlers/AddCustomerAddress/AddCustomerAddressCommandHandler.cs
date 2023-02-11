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

namespace AW.Services.Customer.Core.Handlers.AddCustomerAddress
{
    public class AddCustomerAddressCommandHandler : IRequestHandler<AddCustomerAddressCommand, Unit>
    {
        private readonly ILogger<AddCustomerAddressCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<Entities.Customer> _customerRepository;

        public AddCustomerAddressCommandHandler(
            ILogger<AddCustomerAddressCommandHandler> logger,
            IMapper mapper,
            IRepository<Entities.Customer> customerRepository
        ) => (_logger, _mapper, _customerRepository) = 
            (logger, mapper, customerRepository);

        public async Task<Unit> Handle(AddCustomerAddressCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle called");
            _logger.LogInformation("Getting customer from database");

            var customer = await _customerRepository.SingleOrDefaultAsync(
                new GetCustomerSpecification(request.AccountNumber),
                cancellationToken
            );
            Guard.Against.CustomerNull(customer, request.AccountNumber, _logger);

            _logger.LogInformation("Adding address to customer");
            var customerAddress = _mapper.Map<Entities.CustomerAddress>(request.CustomerAddress);
            customer!.AddAddress(customerAddress);

            _logger.LogInformation("Saving customer to database");
            await _customerRepository.UpdateAsync(customer, cancellationToken);

            return Unit.Value;
        }
    }
}