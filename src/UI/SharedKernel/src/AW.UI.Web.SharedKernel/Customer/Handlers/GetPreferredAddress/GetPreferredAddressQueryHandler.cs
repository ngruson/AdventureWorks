using Ardalis.GuardClauses;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.UI.Web.SharedKernel.Customer.Handlers.GetPreferredAddress
{
    public class GetPreferredAddressQueryHandler : IRequestHandler<GetPreferredAddressQuery, Address>
    {
        private readonly ILogger<GetPreferredAddressQueryHandler> logger;
        private readonly ICustomerApiClient client;

        public GetPreferredAddressQueryHandler(ILogger<GetPreferredAddressQueryHandler> logger, ICustomerApiClient client)
        {
            this.logger = logger;
            this.client = client;
        }

        public async Task<Address> Handle(GetPreferredAddressQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting preferred address from API for {@Query}", request);
            var address = await client.GetPreferredAddressAsync(
                request.AccountNumber,
                request.AddressType
            );
            Guard.Against.Null(address, nameof(address));

            logger.LogInformation("Returning preferred address for {@Query}", request);

            return address;
        }
    }
}