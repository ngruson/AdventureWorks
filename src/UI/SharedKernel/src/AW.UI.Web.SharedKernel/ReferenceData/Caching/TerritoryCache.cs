using AW.SharedKernel.Caching;
using Microsoft.Extensions.Caching.Memory;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using AW.UI.Web.SharedKernel.Caching;

namespace AW.UI.Web.SharedKernel.ReferenceData.Caching
{
    public class TerritoryCache : ICache<Handlers.GetTerritories.Territory>
    {
        private readonly IMemoryCache cache;
        private readonly IReferenceDataApiClient client;
        private List<Handlers.GetTerritories.Territory>? territories;

        public TerritoryCache(IMemoryCache cache, IReferenceDataApiClient client) =>
            (this.cache, this.client) = (cache, client);

        public async Task Initialize()
        {
            territories = await client.GetTerritoriesAsync();

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromHours(1)
            );

            cache.Set(CacheKeys.Territories, territories, cacheEntryOptions);
        }

        public async Task<List<Handlers.GetTerritories.Territory>?> GetData()
        {
            if (!cache.TryGetValue(CacheKeys.Territories, out territories))
            {
                await Initialize();
            }

            return territories;
        }

        public async Task<List<Handlers.GetTerritories.Territory>> GetData(Func<Handlers.GetTerritories.Territory, bool> predicate)
        {
            return (await GetData())!
                .Where(predicate).ToList();
        }
    }
}