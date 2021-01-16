using AW.Core.Abstractions.Api.SalesOrderApi;
using AW.Core.Abstractions.Api.SalesOrderApi.GetSalesOrder;
using AW.Core.Abstractions.Api.SalesOrderApi.ListSalesOrders;
using AW.Infrastructure.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AW.Infrastructure.Api.REST
{
    public class SalesOrderApi : ISalesOrderApi
    {
        private ILogger<SalesOrderApi> logger;
        private IHttpRequestFactory httpRequestFactory;
        private string baseAddress;

        public SalesOrderApi(
            ILogger<SalesOrderApi> logger,
            IHttpRequestFactory httpRequestFactory,
            string baseAddress
        ) => (this.logger, this.httpRequestFactory, this.baseAddress) = (logger, httpRequestFactory, baseAddress);

        public Dictionary<string, string> Headers { get; } = new Dictionary<string, string>();

        public async Task<GetSalesOrderResponse> GetSalesOrderAsync(GetSalesOrderRequest request)
        {
            logger.LogInformation("GET: GetSalesOrder request to SalesOrder API");
            var response = await httpRequestFactory.Get($"{baseAddress}/{request.SalesOrderNumber}");
            logger.LogInformation($"GET: Response = {response.StatusCode} ({response.ReasonPhrase})");

            if (response.IsSuccessStatusCode)
            {
                return response.ContentAsType<GetSalesOrderResponse>();
            }

            return null;
        }

        public async Task<ListSalesOrdersResponse> ListSalesOrdersAsync(ListSalesOrdersRequest request)
        {
            logger.LogInformation("GET: ListSalesOrders request to SalesOrder API");
            var response = await httpRequestFactory.Get($"{baseAddress}", Headers);
            logger.LogInformation($"GET: Response = {response.StatusCode} ({response.ReasonPhrase})");

            if (response.IsSuccessStatusCode)
            {
                return response.ContentAsType<ListSalesOrdersResponse>();
            }

            return null;
        }
    }
}