using AW.SharedKernel.Caching;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.UI.Web.SharedKernel.ReferenceData.Handlers.GetAddressTypes
{
    public class GetAddressTypesQueryHandler : IRequestHandler<GetAddressTypesQuery, List<AddressType>?>
    {
        private readonly ILogger<GetAddressTypesQueryHandler> logger;
        private readonly ICache<AddressType> cache;

        public GetAddressTypesQueryHandler(ILogger<GetAddressTypesQueryHandler> logger, ICache<AddressType> cache) => (this.logger, this.cache) = (logger, cache);

        public async Task<List<AddressType>?> Handle(GetAddressTypesQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting all address types from cache");
            return await cache.GetData();
        }
    }
}