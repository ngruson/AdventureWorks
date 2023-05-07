using Ardalis.GuardClauses;
using AW.SharedKernel.Extensions;
using AW.UI.Web.Infrastructure.Api.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.UI.Web.Infrastructure.Api.Customer.Handlers.GetIndividualCustomer
{
    public class GetIndividualCustomerQueryHandler : IRequestHandler<GetIndividualCustomerQuery, IndividualCustomer>
    {
        private readonly ILogger<GetIndividualCustomerQueryHandler> _logger;
        private readonly ICustomerApiClient _client;

        public GetIndividualCustomerQueryHandler(ILogger<GetIndividualCustomerQueryHandler> logger, ICustomerApiClient client)
        {
            _logger = logger;
            _client = client;
        }

        public async Task<IndividualCustomer> Handle(GetIndividualCustomerQuery request, CancellationToken cancellationToken)
        {
            Guard.Against.NullOrEmpty(request.AccountNumber, _logger);

            _logger.LogInformation("Getting individual customer {AccountNumber} from API", request.AccountNumber);
            var customer = await _client.GetCustomerAsync<IndividualCustomer>(
                request.AccountNumber
            );
            Guard.Against.Null(customer, _logger);

            _logger.LogInformation("Returning individual customer {AccountNumber}", request.AccountNumber);

            return customer!;
        }
    }
}
