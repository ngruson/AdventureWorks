using Ardalis.GuardClauses;
using AW.Services.Customer.Core.GuardClauses;
using AW.Services.Customer.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.Customer.Core.Handlers.DeleteCustomerAddress
{
    public class DeleteCustomerAddressCommandHandler : IRequestHandler<DeleteCustomerAddressCommand, Unit>
    {
        private readonly ILogger<DeleteCustomerAddressCommandHandler> _logger;
        private readonly IRepository<Entities.Customer> _repository;

        public DeleteCustomerAddressCommandHandler(
            ILogger<DeleteCustomerAddressCommandHandler> logger,
            IRepository<Entities.Customer> repository
        ) => (_logger, _repository) = (logger, repository);
        
        public async Task<Unit> Handle(DeleteCustomerAddressCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle called");
            _logger.LogInformation("Getting customer from database");

            var customer = await _repository.SingleOrDefaultAsync(
                new GetCustomerSpecification(request.AccountNumber),
                cancellationToken
            );
            Guard.Against.CustomerNull(customer, request.AccountNumber, _logger);

            _logger.LogInformation("Removing address from customer");
            var customerAddress = customer.Addresses.FirstOrDefault(
                a => a.AddressType == request.AddressType
            );
            Guard.Against.AddressNull(
                customerAddress, 
                request.AccountNumber, 
                request.AddressType, 
                _logger
            );

            customer.RemoveAddress(customerAddress);

            _logger.LogInformation("Updating customer to database");
            await _repository.UpdateAsync(customer, cancellationToken);
            return Unit.Value;
        }
    }
}