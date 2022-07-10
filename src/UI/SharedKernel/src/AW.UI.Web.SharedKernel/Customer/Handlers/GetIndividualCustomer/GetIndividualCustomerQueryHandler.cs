using Ardalis.GuardClauses;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.UI.Web.SharedKernel.Customer.Handlers.GetIndividualCustomer
{
    public class GetIndividualCustomerQueryHandler : IRequestHandler<GetIndividualCustomerQuery, IndividualCustomer>
    {
        private readonly ILogger<GetIndividualCustomerQueryHandler> logger;
        private readonly ICustomerApiClient client;

        public GetIndividualCustomerQueryHandler(ILogger<GetIndividualCustomerQueryHandler> logger, ICustomerApiClient client)
        {
            this.logger = logger;
            this.client = client;
        }

        public async Task<IndividualCustomer> Handle(GetIndividualCustomerQuery request, CancellationToken cancellationToken)
        {
            Guard.Against.NullOrEmpty(request.AccountNumber, nameof(request.AccountNumber));

            logger.LogInformation("Getting individual customer {AccountNumber} from API", request.AccountNumber);
            var customer = await client.GetCustomerAsync<IndividualCustomer>(
                request.AccountNumber
            );
            Guard.Against.Null(customer, nameof(customer));

            logger.LogInformation("Returning individual customer {AccountNumber}", request.AccountNumber);

            return customer;
        }
    }
}