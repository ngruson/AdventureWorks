using AW.SharedKernel.Caching;
using Microsoft.Extensions.Caching.Memory;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using AW.UI.Web.SharedKernel.Caching;

namespace AW.UI.Web.SharedKernel.ReferenceData.Caching
{
    public class StatesProvinceCache : ICache<Handlers.GetStatesProvinces.StateProvince>
    {
        private readonly IMemoryCache cache;
        private readonly IReferenceDataApiClient client;

        public StatesProvinceCache(IMemoryCache cache, IReferenceDataApiClient client) =>
            (this.cache, this.client) = (cache, client);

        public async Task<List<Handlers.GetStatesProvinces.StateProvince>?> Initialize()
        {
            var statesProvinces = await client.GetStatesProvincesAsync();

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromHours(1)
            );

            cache.Set(CacheKeys.StatesProvinces, statesProvinces, cacheEntryOptions);

            return statesProvinces;
        }

        public async Task<List<Handlers.GetStatesProvinces.StateProvince>?> GetData()
        {
            if (!cache.TryGetValue(CacheKeys.StatesProvinces, out List<Handlers.GetStatesProvinces.StateProvince>? statesProvinces))
            {
                statesProvinces = await Initialize();
            }

            return statesProvinces;
        }

        public async Task<List<Handlers.GetStatesProvinces.StateProvince>?> GetData(Func<Handlers.GetStatesProvinces.StateProvince, bool> predicate)
        {
            return (await GetData())!
                .Where(predicate).ToList();
        }
    }
}