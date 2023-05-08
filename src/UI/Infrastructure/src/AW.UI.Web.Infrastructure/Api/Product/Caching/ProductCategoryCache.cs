﻿using AW.SharedKernel.Caching;
using AW.UI.Web.Infrastructure.Api.Interfaces;
using AW.UI.Web.Infrastructure.Api.Product.Handlers.GetProductCategories;
using AW.UI.Web.SharedKernel.Caching;
using Microsoft.Extensions.Caching.Memory;

namespace AW.UI.Web.Infrastructure.Api.Product.Caching
{
    public class ProductCategoryCache : ICache<ProductCategory>
    {
        private readonly IMemoryCache cache;
        private readonly IProductApiClient client;

        public ProductCategoryCache(IMemoryCache cache, IProductApiClient client) =>
            (this.cache, this.client) = (cache, client);

        public async Task<List<ProductCategory>?> GetData()
        {

            if (!cache.TryGetValue(CacheKeys.ProductCategories, out List<ProductCategory>? categories))
            {
                categories = await Initialize();
            }

            return categories;
        }

        public async Task<List<ProductCategory>?> GetData(Func<ProductCategory, bool> predicate)
        {
            return (await GetData())?
                .Where(predicate).ToList();
        }

        private async Task<List<ProductCategory>?> Initialize()
        {
            var categories = await client.GetCategories();

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromHours(1)
            );

            cache.Set(CacheKeys.ProductCategories, categories, cacheEntryOptions);

            return categories;
        }
    }
}