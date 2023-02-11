using AW.SharedKernel.Caching;
using Microsoft.Extensions.Caching.Memory;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using AW.UI.Web.SharedKernel.Caching;

namespace AW.UI.Web.SharedKernel.ReferenceData.Caching
{
    public class CountryCache : ICache<Handlers.GetCountries.CountryRegion>
    {
        private readonly IMemoryCache cache;
        private readonly IReferenceDataApiClient client;

        public CountryCache(IMemoryCache cache, IReferenceDataApiClient client) =>
            (this.cache, this.client) = (cache, client);

        public async Task<List<Handlers.GetCountries.CountryRegion>> Initialize()
        {
            var countries = await client.GetCountriesAsync();

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromHours(1)
            );

            cache.Set(CacheKeys.Countries, countries, cacheEntryOptions);

            return countries!;
        }

        public async Task<List<Handlers.GetCountries.CountryRegion>?> GetData()
        {
            if (!cache.TryGetValue(CacheKeys.Countries, out List<Handlers.GetCountries.CountryRegion>? countries))
            {
                countries = await Initialize();
            }

            return countries;
        }

        public async Task<List<Handlers.GetCountries.CountryRegion>?> GetData(Func<Handlers.GetCountries.CountryRegion, bool> predicate)
        {
            return (await GetData())!
                .Where(predicate).ToList();
        }
    }
}
