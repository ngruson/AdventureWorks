using AW.SharedKernel.Caching;
using Microsoft.Extensions.Caching.Memory;
using AW.UI.Web.Infrastructure.Api.Interfaces;
using AW.UI.Web.SharedKernel.Caching;
using AW.UI.Web.Infrastructure.Api.ReferenceData.Handlers.GetShipMethods;

namespace AW.UI.Web.Infrastructure.Api.ReferenceData.Caching
{
    public class ShipMethodCache : ICache<ShipMethod>
    {
        private readonly IMemoryCache cache;
        private readonly IReferenceDataApiClient client;

        public ShipMethodCache(IMemoryCache cache, IReferenceDataApiClient client) =>
            (this.cache, this.client) = (cache, client);

        public async Task<List<ShipMethod>> Initialize()
        {
            var shipMethods = await client.GetShipMethodsAsync();

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromHours(1)
            );

            cache.Set(CacheKeys.ShipMethods, shipMethods, cacheEntryOptions);

            return shipMethods!;
        }

        public async Task<List<ShipMethod>?> GetData()
        {
            if (!cache.TryGetValue(CacheKeys.ShipMethods, out List<ShipMethod>? shipMethods))
            {
                shipMethods = await Initialize();
            }

            return shipMethods;
        }

        public async Task<List<ShipMethod>?> GetData(Func<ShipMethod, bool> predicate)
        {
            return (await GetData())!
                .Where(predicate).ToList();
        }
    }
}
