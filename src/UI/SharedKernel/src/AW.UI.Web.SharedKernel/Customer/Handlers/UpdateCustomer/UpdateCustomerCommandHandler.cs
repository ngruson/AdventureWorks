using Ardalis.GuardClauses;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.UI.Web.SharedKernel.Customer.Handlers.UpdateCustomer
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Customer>
    {
        private readonly ILogger<UpdateCustomerCommandHandler> logger;
        private readonly ICustomerApiClient client;

        public UpdateCustomerCommandHandler(ILogger<UpdateCustomerCommandHandler> logger, ICustomerApiClient client)
        {
            this.logger = logger;
            this.client = client;
        }

        public async Task<Customer> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            Guard.Against.NullOrEmpty(request.AccountNumber, nameof(request.AccountNumber));
            Guard.Against.Null(request.Customer, nameof(request.Customer));

            logger.LogInformation("Updating customer {AccountNumber}", request.AccountNumber);
            var customer = await client.UpdateCustomerAsync(
                request.AccountNumber,
                request.Customer
            );
            Guard.Against.Null(customer, nameof(customer));

            logger.LogInformation("Returning updated customer {AccountNumber}", request.AccountNumber);

            return customer;
        }
    }
}