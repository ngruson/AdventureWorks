using AW.SharedKernel.Caching;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.UI.Web.SharedKernel.ReferenceData.Handlers.GetCountries
{
    public class GetCountriesQueryHandler : IRequestHandler<GetCountriesQuery, List<CountryRegion>?>
    {
        private readonly ILogger<GetCountriesQueryHandler> logger;
        private readonly ICache<CountryRegion> cache;

        public GetCountriesQueryHandler(ILogger<GetCountriesQueryHandler> logger, ICache<CountryRegion> cache) => (this.logger, this.cache) = (logger, cache);

        public async Task<List<CountryRegion>?> Handle(GetCountriesQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting all countries from cache");
            return await cache.GetData();
        }
    }
}