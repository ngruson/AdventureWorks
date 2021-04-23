using Ardalis.GuardClauses;
using Ardalis.Specification;
using AW.Services.Customer.Application.Specifications;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.Customer.Application.DeleteCustomerAddress
{
    public class DeleteCustomerAddressCommandHandler : IRequestHandler<DeleteCustomerAddressCommand, Unit>
    {
        private readonly ILogger<DeleteCustomerAddressCommandHandler> logger;
        private readonly IRepositoryBase<Domain.Customer> customerRepository;

        public DeleteCustomerAddressCommandHandler(
            ILogger<DeleteCustomerAddressCommandHandler> logger,
            IRepositoryBase<Domain.Customer> customerRepository
        ) => (this.logger, this.customerRepository) = (logger, customerRepository);
        
        public async Task<Unit> Handle(DeleteCustomerAddressCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handle called");
            logger.LogInformation("Getting customer from database");

            var customer = await customerRepository.GetBySpecAsync(
                new GetCustomerSpecification(request.AccountNumber)
            );
            Guard.Against.Null(customer, nameof(customer), logger);

            logger.LogInformation("Removing address from customer");
            var customerAddress = customer.Addresses.FirstOrDefault(
                a => a.AddressType == request.CustomerAddress.AddressType
            );
            Guard.Against.Null(customerAddress, nameof(customerAddress), logger);

            customer.Addresses.Remove(customerAddress);

            logger.LogInformation("Updating customer to database");
            await customerRepository.UpdateAsync(customer);
            return Unit.Value;
        }
    }
}