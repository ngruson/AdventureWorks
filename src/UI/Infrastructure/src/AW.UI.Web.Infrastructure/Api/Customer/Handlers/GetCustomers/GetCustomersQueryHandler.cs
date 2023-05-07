using Ardalis.GuardClauses;
using AW.SharedKernel.Extensions;
using AW.UI.Web.Infrastructure.Api.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.UI.Web.Infrastructure.Api.Customer.Handlers.GetCustomers
{
    public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, GetCustomersResponse?>
    {
        private readonly ILogger<GetCustomersQueryHandler> _logger;
        private readonly ICustomerApiClient _client;

        public GetCustomersQueryHandler(ILogger<GetCustomersQueryHandler> logger, ICustomerApiClient client)
        {
            _logger = logger;
            _client = client;
        }

        public async Task<GetCustomersResponse?> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting customers from API with {@Query}", request);
            var customers = await _client.GetCustomersAsync(
                request.PageIndex,
                request.PageSize,
                request.Territory,
                request.CustomerType,
                request.AccountNumber
            );
            Guard.Against.Null(customers, _logger);

            _logger.LogInformation("Returning customers for {@Query}", request);

            return customers;
        }
    }
}
