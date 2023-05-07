using AW.SharedKernel.Caching;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.UI.Web.Infrastructure.Api.ReferenceData.Handlers.GetShipMethods
{
    public class GetShipMethodsQueryHandler : IRequestHandler<GetShipMethodsQuery, List<ShipMethod>?>
    {
        private readonly ILogger<GetShipMethodsQueryHandler> logger;
        private readonly ICache<ShipMethod> cache;

        public GetShipMethodsQueryHandler(ILogger<GetShipMethodsQueryHandler> logger, ICache<ShipMethod> cache) => (this.logger, this.cache) = (logger, cache);

        public async Task<List<ShipMethod>?> Handle(GetShipMethodsQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting all sales persons from cache");
            return await cache.GetData();
        }
    }
}