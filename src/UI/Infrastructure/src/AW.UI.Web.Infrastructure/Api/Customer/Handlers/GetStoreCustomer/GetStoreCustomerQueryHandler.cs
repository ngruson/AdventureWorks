using Ardalis.GuardClauses;
using AW.SharedKernel.Extensions;
using AW.UI.Web.Infrastructure.Api.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.UI.Web.Infrastructure.Api.Customer.Handlers.GetStoreCustomer
{
    public class GetStoreCustomerQueryHandler : IRequestHandler<GetStoreCustomerQuery, StoreCustomer>
    {
        private readonly ILogger<GetStoreCustomerQueryHandler> _logger;
        private readonly ICustomerApiClient _client;

        public GetStoreCustomerQueryHandler(ILogger<GetStoreCustomerQueryHandler> logger, ICustomerApiClient client)
        {
            _logger = logger;
            _client = client;
        }

        public async Task<StoreCustomer> Handle(GetStoreCustomerQuery request, CancellationToken cancellationToken)
        {
            Guard.Against.NullOrEmpty(request.AccountNumber, _logger);

            _logger.LogInformation("Getting store customer {AccountNumber} from API", request.AccountNumber);
            var customer = await _client.GetCustomerAsync<StoreCustomer>(
                request.AccountNumber
            );
            Guard.Against.Null(customer, _logger);

            _logger.LogInformation("Returning store customer {AccountNumber}", request.AccountNumber);

            return customer!;
        }
    }
}
