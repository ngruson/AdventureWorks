using Ardalis.GuardClauses;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.UI.Web.SharedKernel.Customer.Handlers.GetCustomer
{
    public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, Customer>
    {
        private readonly ILogger<GetCustomerQueryHandler> logger;
        private readonly ICustomerApiClient client;

        public GetCustomerQueryHandler(ILogger<GetCustomerQueryHandler> logger, ICustomerApiClient client)
        {
            this.logger = logger;
            this.client = client;
        }

        public async Task<Customer> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            Guard.Against.NullOrEmpty(request.AccountNumber, nameof(request.AccountNumber));

            logger.LogInformation("Getting customer {AccountNumber} from API", request.AccountNumber);
            var customer = await client.GetCustomerAsync(
                request.AccountNumber
            );
            Guard.Against.Null(customer, nameof(customer));

            logger.LogInformation("Returning customer {AccountNumber}", request.AccountNumber);

            return customer;
        }
    }
}