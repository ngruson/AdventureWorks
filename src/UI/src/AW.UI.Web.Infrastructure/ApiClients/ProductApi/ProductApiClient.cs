using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AW.UI.Web.Infrastructure.ApiClients.ProductApi
{
    public class ProductApiClient : IProductApiClient
    {
        private readonly HttpClient client;
        private readonly ILogger logger;

        public ProductApiClient(HttpClient client, ILogger<ProductApiClient> logger) =>
            (this.client, this.logger) = (client, logger);

        public async Task<List<Models.ProductCategory>> GetCategoriesAsync()
        {
            string requestUri = "/product-api/ProductCategory?api-version=1.0";

            logger.LogInformation("Getting product categories");

            using var response = await client.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();

            return await stream.DeserializeAsync<List<Models.ProductCategory>>(
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

        public async Task<Models.GetProductsResult> GetProductsAsync(
            int pageIndex,
            int pageSize,
            string category,
            string subcategory,
            string orderBy
        )
        {
            //pageIndex=0&pageSize=10&category=Bikes&orderBy=asc%28productNumber%29&api-version=1.0"
            string requestUri = $"/product-api/Product?api-version=1.0&pageIndex={pageIndex}&pageSize={pageSize}";
            string logMessage = "Getting products with page index {PageIndex}, page size {PageSize}";

            var args = new List<object> { pageIndex, pageSize };

            if (!string.IsNullOrEmpty(category))
            {
                logMessage += ", category {Category}";
                args.Add(category);
                requestUri += $"&category={category}";
            };
            if (!string.IsNullOrEmpty(subcategory))
            {
                logMessage += ", subcategory {Subcategory}";
                args.Add(subcategory);
                requestUri += $"&subcategory={subcategory}";
            };
            if (!string.IsNullOrEmpty(orderBy))
            {
                logMessage += ", orderBy {OrderBy}";
                args.Add(orderBy);
                requestUri += $"&orderBy={orderBy}";
            };

            logger.LogInformation(logMessage, args.ToArray());

            using var response = await client.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();

            return await stream.DeserializeAsync<Models.GetProductsResult>(
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