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
            logger.LogInformation("Updating customer {AccountNumber}", request.AccountNumber);
            var address = await client.UpdateCustomerAsync(
                request.AccountNumber,
                request.Customer
            );
            Guard.Against.Null(address, nameof(address));

            logger.LogInformation("Returning updated customer {AccountNumber}", request.AccountNumber);

            return address;
        }
    }
}