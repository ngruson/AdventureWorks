using Ardalis.GuardClauses;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.UI.Web.SharedKernel.Customer.Handlers.GetStoreCustomer
{
    public class GetStoreCustomerQueryHandler : IRequestHandler<GetStoreCustomerQuery, StoreCustomer>
    {
        private readonly ILogger<GetStoreCustomerQueryHandler> logger;
        private readonly ICustomerApiClient client;

        public GetStoreCustomerQueryHandler(ILogger<GetStoreCustomerQueryHandler> logger, ICustomerApiClient client)
        {
            this.logger = logger;
            this.client = client;
        }

        public async Task<StoreCustomer> Handle(GetStoreCustomerQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting store customer {AccountNumber} from API", request.AccountNumber);
            var customer = await client.GetCustomerAsync<StoreCustomer>(
                request.AccountNumber
            );
            Guard.Against.Null(customer, nameof(customer));

            logger.LogInformation("Returning store customer {AccountNumber}", request.AccountNumber);

            return customer;
        }
    }
}