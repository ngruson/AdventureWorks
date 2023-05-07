using Ardalis.GuardClauses;
using AW.SharedKernel.Extensions;
using AW.UI.Web.Infrastructure.Api.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.UI.Web.Infrastructure.Api.Customer.Handlers.GetCustomer
{
    public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, Customer?>
    {
        private readonly ILogger<GetCustomerQueryHandler> _logger;
        private readonly ICustomerApiClient _client;

        public GetCustomerQueryHandler(ILogger<GetCustomerQueryHandler> logger, ICustomerApiClient client)
        {
            _logger = logger;
            _client = client;
        }

        public async Task<Customer?> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            Guard.Against.NullOrEmpty(request.AccountNumber, _logger);

            _logger.LogInformation("Getting customer {AccountNumber} from API", request.AccountNumber);
            var customer = await _client.GetCustomerAsync(
                request.AccountNumber
            );
            Guard.Against.Null(customer, _logger);

            _logger.LogInformation("Returning customer {AccountNumber}", request.AccountNumber);

            return customer;
        }
    }
}
