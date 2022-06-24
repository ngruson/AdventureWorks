using AW.SharedKernel.Caching;
using AW.UI.Web.SharedKernel.Caching;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using Microsoft.Extensions.Caching.Memory;

namespace AW.UI.Web.SharedKernel.SalesPerson.Caching
{
    public class SalesPersonCache : ICache<Handlers.GetSalesPersons.SalesPerson>
    {
        private readonly IMemoryCache cache;
        private readonly ISalesPersonApiClient client;
        private List<Handlers.GetSalesPersons.SalesPerson>? salesPersons;

        public SalesPersonCache(IMemoryCache cache, ISalesPersonApiClient client) =>
            (this.cache, this.client) = (cache, client);

        public async Task Initialize()
        {
            salesPersons = await client.GetSalesPersonsAsync();

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromHours(1)
            );

            cache.Set(CacheKeys.SalesPersons, salesPersons, cacheEntryOptions);
        }

        public async Task<List<Handlers.GetSalesPersons.SalesPerson>?> GetData()
        {
            if (!cache.TryGetValue(CacheKeys.SalesPersons, out salesPersons))
            {
                await Initialize();
            }

            return salesPersons;
        }

        public async Task<List<Handlers.GetSalesPersons.SalesPerson>> GetData(Func<Handlers.GetSalesPersons.SalesPerson, bool> predicate)
        {
            var salesPersons = await GetData();
            return salesPersons!.Where(predicate).ToList();
        }
    }
}