using AW.SharedKernel.Caching;
using Microsoft.Extensions.Caching.Memory;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using AW.UI.Web.SharedKernel.Caching;

namespace AW.UI.Web.SharedKernel.ReferenceData.Caching
{
    public class ContactTypeCache : ICache<Handlers.GetContactTypes.ContactType>
    {
        private readonly IMemoryCache cache;
        private readonly IReferenceDataApiClient client;        

        public ContactTypeCache(IMemoryCache cache, IReferenceDataApiClient client) =>
            (this.cache, this.client) = (cache, client);

        public async Task<List<Handlers.GetContactTypes.ContactType>> Initialize()
        {
            var contactTypes = await client.GetContactTypesAsync();

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromHours(1)
            );

            cache.Set(CacheKeys.ContactTypes, contactTypes, cacheEntryOptions);

            return contactTypes;
        }

        public async Task<List<Handlers.GetContactTypes.ContactType>?> GetData()
        {
            if (!cache.TryGetValue(CacheKeys.ContactTypes, out List<Handlers.GetContactTypes.ContactType> contactTypes))
            {
                contactTypes = await Initialize();
            }

            return contactTypes;
        }

        public async Task<List<Handlers.GetContactTypes.ContactType>?> GetData(Func<Handlers.GetContactTypes.ContactType, bool> predicate)
        {
            return (await GetData())!
                .Where(predicate).ToList();
        }
    }
}