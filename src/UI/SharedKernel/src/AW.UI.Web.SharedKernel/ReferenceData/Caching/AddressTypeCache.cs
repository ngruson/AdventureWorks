using AW.SharedKernel.Caching;
using Microsoft.Extensions.Caching.Memory;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using AW.UI.Web.SharedKernel.Caching;

namespace AW.UI.Web.SharedKernel.ReferenceData.Caching
{
    public class AddressTypeCache : ICache<Handlers.GetAddressTypes.AddressType>
    {
        private readonly IMemoryCache cache;
        private readonly IReferenceDataApiClient client;

        public AddressTypeCache(IMemoryCache cache, IReferenceDataApiClient client) =>
            (this.cache, this.client) = (cache, client);

        private async Task<List<Handlers.GetAddressTypes.AddressType>> Initialize()
        {
            var addressTypes = await client.GetAddressTypesAsync();

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromHours(1)
            );

            cache.Set(CacheKeys.AddressTypes, addressTypes, cacheEntryOptions);

            return addressTypes!;
        }

        public async Task<List<Handlers.GetAddressTypes.AddressType>?> GetData()
        {
            if (!cache.TryGetValue(CacheKeys.AddressTypes, out List<Handlers.GetAddressTypes.AddressType>? addressTypes))
            {
                addressTypes = await Initialize();
            }

            return addressTypes;
        }

        public async Task<List<Handlers.GetAddressTypes.AddressType>?> GetData(Func<Handlers.GetAddressTypes.AddressType, bool> predicate)
        {
            return (await GetData())!
                .Where(predicate).ToList();
        }
    }
}
