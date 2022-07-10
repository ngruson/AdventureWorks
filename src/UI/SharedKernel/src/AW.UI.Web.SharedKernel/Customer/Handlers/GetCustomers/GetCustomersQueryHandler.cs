using Ardalis.GuardClauses;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.UI.Web.SharedKernel.Customer.Handlers.GetCustomers
{
    public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, GetCustomersResponse>
    {
        private readonly ILogger<GetCustomersQueryHandler> logger;
        private readonly ICustomerApiClient client;

        public GetCustomersQueryHandler(ILogger<GetCustomersQueryHandler> logger, ICustomerApiClient client)
        {
            this.logger = logger;
            this.client = client;
        }

        public async Task<GetCustomersResponse> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting customers from API with {@Query}", request);
            var customers = await client.GetCustomersAsync(
                request.PageIndex,
                request.PageSize,
                request.Territory,
                request.CustomerType,
                request.AccountNumber
            );
            Guard.Against.Null(customers, nameof(customers));

            logger.LogInformation("Returning customers for {@Query}", request);

            return customers;
        }
    }
}