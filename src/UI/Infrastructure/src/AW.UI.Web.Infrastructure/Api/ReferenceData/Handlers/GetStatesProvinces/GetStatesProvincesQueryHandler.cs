using AW.SharedKernel.Caching;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.UI.Web.Infrastructure.Api.ReferenceData.Handlers.GetStatesProvinces
{
    public class GetStatesProvincesQueryHandler : IRequestHandler<GetStatesProvincesQuery, List<StateProvince>?>
    {
        private readonly ILogger<GetStatesProvincesQueryHandler> logger;
        private readonly ICache<StateProvince> cache;

        public GetStatesProvincesQueryHandler(ILogger<GetStatesProvincesQueryHandler> logger, ICache<StateProvince> cache) => (this.logger, this.cache) = (logger, cache);

        public async Task<List<StateProvince>?> Handle(GetStatesProvincesQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting all states/provinces from cache");
            return await cache.GetData(t => t.CountryRegionCode == request.CountryRegionCode);
        }
    }
}