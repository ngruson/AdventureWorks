using AW.SharedKernel.Caching;
using Microsoft.Extensions.Caching.Memory;
using AW.UI.Web.Infrastructure.Api.Interfaces;
using AW.UI.Web.SharedKernel.Caching;
using AW.UI.Web.Infrastructure.Api.ReferenceData.Handlers.GetCountries;

namespace AW.UI.Web.Infrastructure.Api.ReferenceData.Caching
{
    public class CountryCache : ICache<CountryRegion>
    {
        private readonly IMemoryCache cache;
        private readonly IReferenceDataApiClient client;

        public CountryCache(IMemoryCache cache, IReferenceDataApiClient client) =>
            (this.cache, this.client) = (cache, client);

        public async Task<List<CountryRegion>> Initialize()
        {
            var countries = await client.GetCountriesAsync();

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromHours(1)
            );

            cache.Set(CacheKeys.Countries, countries, cacheEntryOptions);

            return countries!;
        }

        public async Task<List<CountryRegion>?> GetData()
        {
            if (!cache.TryGetValue(CacheKeys.Countries, out List<CountryRegion>? countries))
            {
                countries = await Initialize();
            }

            return countries;
        }

        public async Task<List<CountryRegion>?> GetData(Func<CountryRegion, bool> predicate)
        {
            return (await GetData())!
                .Where(predicate).ToList();
        }
    }
}
