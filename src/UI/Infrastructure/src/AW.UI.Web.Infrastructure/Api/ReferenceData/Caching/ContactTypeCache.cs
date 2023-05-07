using AW.SharedKernel.Caching;
using Microsoft.Extensions.Caching.Memory;
using AW.UI.Web.Infrastructure.Api.Interfaces;
using AW.UI.Web.SharedKernel.Caching;
using AW.UI.Web.Infrastructure.Api.ReferenceData.Handlers.GetContactTypes;

namespace AW.UI.Web.Infrastructure.Api.ReferenceData.Caching
{
    public class ContactTypeCache : ICache<ContactType>
    {
        private readonly IMemoryCache cache;
        private readonly IReferenceDataApiClient client;

        public ContactTypeCache(IMemoryCache cache, IReferenceDataApiClient client) =>
            (this.cache, this.client) = (cache, client);

        public async Task<List<ContactType>> Initialize()
        {
            var contactTypes = await client.GetContactTypesAsync();

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromHours(1)
            );

            cache.Set(CacheKeys.ContactTypes, contactTypes, cacheEntryOptions);

            return contactTypes!;
        }

        public async Task<List<ContactType>?> GetData()
        {
            if (!cache.TryGetValue(CacheKeys.ContactTypes, out List<ContactType>? contactTypes))
            {
                contactTypes = await Initialize();
            }

            return contactTypes;
        }

        public async Task<List<ContactType>?> GetData(Func<ContactType, bool> predicate)
        {
            return (await GetData())!
                .Where(predicate).ToList();
        }
    }
}
