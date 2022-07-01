using AW.SharedKernel.Caching;
using Microsoft.Extensions.Caching.Memory;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using AW.UI.Web.SharedKernel.Caching;

namespace AW.UI.Web.SharedKernel.ReferenceData.Caching
{
    public class ShipMethodCache : ICache<Handlers.GetShipMethods.ShipMethod>
    {
        private readonly IMemoryCache cache;
        private readonly IReferenceDataApiClient client;
        private List<Handlers.GetShipMethods.ShipMethod>? shipMethods;

        public ShipMethodCache(IMemoryCache cache, IReferenceDataApiClient client) =>
            (this.cache, this.client) = (cache, client);

        public async Task Initialize()
        {
            shipMethods = await client.GetShipMethodsAsync();

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromHours(1)
            );

            cache.Set(CacheKeys.ShipMethods, shipMethods, cacheEntryOptions);
        }

        public async Task<List<Handlers.GetShipMethods.ShipMethod>?> GetData()
        {
            if (!cache.TryGetValue(CacheKeys.ShipMethods, out shipMethods))
            {
                await Initialize();
            }

            return shipMethods;
        }

        public async Task<List<Handlers.GetShipMethods.ShipMethod>> GetData(Func<Handlers.GetShipMethods.ShipMethod, bool> predicate)
        {
            return (await GetData())!
                .Where(predicate).ToList();
        }
    }
}