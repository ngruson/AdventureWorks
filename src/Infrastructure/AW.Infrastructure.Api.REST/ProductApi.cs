using AW.Core.Abstractions.Api.ProductApi;
using AW.Core.Abstractions.Api.ProductApi.GetProduct;
using AW.Core.Abstractions.Api.ProductApi.ListProducts;
using AW.Infrastructure.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AW.Infrastructure.Api.REST
{
    public class ProductApi : IProductApi
    {
        private ILogger<ProductApi> logger;
        private IHttpRequestFactory httpRequestFactory;
        private string baseAddress;

        public ProductApi(
            ILogger<ProductApi> logger,
            IHttpRequestFactory httpRequestFactory,
            string baseAddress
        ) => (this.logger, this.httpRequestFactory, this.baseAddress) = (logger, httpRequestFactory, baseAddress);

        public Dictionary<string, string> Headers { get; } = new Dictionary<string, string>();

        public async Task<GetProductResponse> GetProduct(GetProductRequest request)
        {
            logger.LogInformation("GET: GetProduct request to Product API");
            var response = await httpRequestFactory.Get($"{baseAddress}/{request.ProductNumber}", Headers);
            logger.LogInformation($"GET: Response = {response.StatusCode} ({response.ReasonPhrase})");

            if (response.IsSuccessStatusCode)
            {
                return response.ContentAsType<GetProductResponse>();
            }

            return null;
        }

        public async Task<ListProductsResponse> ListProductsAsync(ListProductsRequest request)
        {
            logger.LogInformation("GET: ListProducts request to Product API");
            var response = await httpRequestFactory.Get($"{baseAddress}/?pageIndex={request.PageIndex}&pageSize={request.PageSize}", Headers);
            logger.LogInformation($"GET: Response = {response.StatusCode} ({response.ReasonPhrase})");

            if (response.IsSuccessStatusCode)
            {
                return response.ContentAsType<ListProductsResponse>();
            }

            return null;
        }
    }
}