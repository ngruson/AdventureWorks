using Ardalis.GuardClauses;
using AW.Services.Customer.Core.Specifications;
using AW.SharedKernel.Extensions;
using AW.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.Customer.Core.Handlers.DeleteCustomerAddress
{
    public class DeleteCustomerAddressCommandHandler : IRequestHandler<DeleteCustomerAddressCommand, Unit>
    {
        private readonly ILogger<DeleteCustomerAddressCommandHandler> logger;
        private readonly IRepository<Entities.Customer> customerRepository;

        public DeleteCustomerAddressCommandHandler(
            ILogger<DeleteCustomerAddressCommandHandler> logger,
            IRepository<Entities.Customer> customerRepository
        ) => (this.logger, this.customerRepository) = (logger, customerRepository);
        
        public async Task<Unit> Handle(DeleteCustomerAddressCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handle called");
            logger.LogInformation("Getting customer from database");

            var customer = await customerRepository.GetBySpecAsync(
                new GetCustomerSpecification(request.AccountNumber),
                cancellationToken
            );
            Guard.Against.Null(customer, nameof(customer), logger);

            logger.LogInformation("Removing address from customer");
            var customerAddress = customer.Addresses.FirstOrDefault(
                a => a.AddressType == request.AddressType
            );
            Guard.Against.Null(customerAddress, nameof(customerAddress), logger);

            customer.Addresses.Remove(customerAddress);

            logger.LogInformation("Updating customer to database");
            await customerRepository.UpdateAsync(customer, cancellationToken);
            return Unit.Value;
        }
    }
}