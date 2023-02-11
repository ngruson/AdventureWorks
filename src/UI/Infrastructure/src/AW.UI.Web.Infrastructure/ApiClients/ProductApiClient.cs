using AW.UI.Web.SharedKernel.Interfaces.Api;
using AW.UI.Web.SharedKernel.Product.Handlers.GetProductCategories;
using AW.UI.Web.SharedKernel.Product.Handlers.GetProducts;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AW.UI.Web.Infrastructure.ApiClients
{
    public class ProductApiClient : IProductApiClient
    {
        private readonly HttpClient client;
        private readonly ILogger<ProductApiClient?> logger;

        public ProductApiClient(HttpClient client, ILogger<ProductApiClient?> logger) =>
            (this.client, this.logger) = (client, logger);

        public async Task<List<ProductCategory>?> GetCategoriesAsync()
        {
            string requestUri = "/product-api/ProductCategory?api-version=1.0";

            logger.LogInformation("Getting product categories");

            using var response = await client.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();

            return await stream.DeserializeAsync<List<ProductCategory>?>(
                new JsonSerializerOptions
                {
                    Converters =
                    {
                        new JsonStringEnumConverter()
                    },
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                }
            );
        }

        public async Task<GetProductsResult?> GetProductsAsync(
            int pageIndex,
            int pageSize,
            string? category,
            string? subcategory,
            string? orderBy
        )
        {
            string requestUri = $"/product-api/Product?api-version=1.0&pageIndex={pageIndex}&pageSize={pageSize}";
            string logMessage = "Getting products with page index {PageIndex}, page size {PageSize}";

            var args = new List<object?> { pageIndex, pageSize };

            if (!string.IsNullOrEmpty(category))
            {
                logMessage += ", category {Category}";
                args.Add(category);
                requestUri += $"&category={category}";
            }
            if (!string.IsNullOrEmpty(subcategory))
            {
                logMessage += ", subcategory {Subcategory}";
                args.Add(subcategory);
                requestUri += $"&subcategory={subcategory}";
            }
            if (!string.IsNullOrEmpty(orderBy))
            {
                logMessage += ", orderBy {OrderBy}";
                args.Add(orderBy);
                requestUri += $"&orderBy={orderBy}";
            }

            logger.LogInformation(logMessage, args.ToArray());

            using var response = await client.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();

            return await stream.DeserializeAsync<GetProductsResult?>(
                new JsonSerializerOptions
                {
                    Converters =
                    {
                        new JsonStringEnumConverter()
                    },
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                }
            );
        }

        public async Task<SharedKernel.Product.Handlers.GetProduct.Product?> GetProductAsync(string? productNumber)
        {
            string requestUri = $"/product-api/Product/{productNumber}?api-version=1.0";
            logger.LogInformation("Getting product with product number {ProductNumber}", productNumber);

            using var response = await client.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();

            return await stream.DeserializeAsync<SharedKernel.Product.Handlers.GetProduct.Product?>(
                new JsonSerializerOptions
                {
                    Converters =
                    {
                        new JsonStringEnumConverter()
                    },
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                }
            );
        }
    }
}