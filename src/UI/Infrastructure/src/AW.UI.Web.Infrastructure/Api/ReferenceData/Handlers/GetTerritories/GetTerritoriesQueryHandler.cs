using AW.SharedKernel.Caching;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.UI.Web.Infrastructure.Api.ReferenceData.Handlers.GetTerritories
{
    public class GetTerritoriesQueryHandler : IRequestHandler<GetTerritoriesQuery, List<Territory>?>
    {
        private readonly ILogger<GetTerritoriesQueryHandler> logger;
        private readonly ICache<Territory> cache;

        public GetTerritoriesQueryHandler(ILogger<GetTerritoriesQueryHandler> logger, ICache<Territory> cache) => (this.logger, this.cache) = (logger, cache);

        public async Task<List<Territory>?> Handle(GetTerritoriesQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.CountryRegionCode))
            {
                logger.LogInformation("Getting all territories from cache");
                return await cache.GetData();
            }
            else
            {
                logger.LogInformation("Getting territories for {CountryRegionCode} from cache", request.CountryRegionCode);
                return await cache.GetData(t => t.CountryRegionCode == request.CountryRegionCode);
            }
        }
    }
}