using Ardalis.GuardClauses;
using AW.SharedKernel.Extensions;
using AW.UI.Web.Infrastructure.Api.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.UI.Web.Infrastructure.Api.Customer.Handlers.GetPreferredAddress
{
    public class GetPreferredAddressQueryHandler : IRequestHandler<GetPreferredAddressQuery, Address>
    {
        private readonly ILogger<GetPreferredAddressQueryHandler> _logger;
        private readonly ICustomerApiClient _client;

        public GetPreferredAddressQueryHandler(ILogger<GetPreferredAddressQueryHandler> logger, ICustomerApiClient client)
        {
            _logger = logger;
            _client = client;
        }

        public async Task<Address> Handle(GetPreferredAddressQuery request, CancellationToken cancellationToken)
        {
            Guard.Against.NullOrEmpty(request.AccountNumber, _logger);
            Guard.Against.NullOrEmpty(request.AddressType, _logger);

            _logger.LogInformation("Getting preferred address from API for {@Query}", request);
            var address = await _client.GetPreferredAddressAsync(
                request.AccountNumber,
                request.AddressType
            );
            Guard.Against.Null(address, _logger);

            _logger.LogInformation("Returning preferred address for {@Query}", request);

            return address!;
        }
    }
}
