using AW.UI.Web.Common.ApiClients.ProductApi;
using AW.UI.Web.Common.ApiClients.ProductApi.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AW.UI.Web.Store.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductApiClient productApiClient;
        private readonly ILogger<ProductService> logger;

        public ProductService(IProductApiClient productApiClient, ILogger<ProductService> logger) =>
            (this.productApiClient, this.logger) = (productApiClient, logger);

        public async Task<List<ProductCategory>> GetCategoriesAsync()
        {
            logger.LogInformation("GetCategories called");
            var response = await productApiClient.GetCategoriesAsync();

            return response;
        }

        public async Task<GetProductsResult> GetProductsAsync(int pageIndex, int pageSize, string category, string subcategory)
        {
            logger.LogInformation("GetProducts called");
            var products = await productApiClient.GetProductsAsync(
                    pageIndex, pageSize, category, subcategory, "asc(productNumber)"
            );

            return products;            
        }
    }
}