using AW.Core.Abstractions.Api.StateProvinceApi;
using AW.Core.Abstractions.Api.StateProvinceApi.ListStateProvinces;
using AW.Infrastructure.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AW.Infrastructure.Api.REST
{
    public class StateProvinceApi : IStateProvinceApi
    {
        private ILogger<StateProvinceApi> logger;
        private IHttpRequestFactory httpRequestFactory;
        private string baseAddress;

        public StateProvinceApi(
            ILogger<StateProvinceApi> logger,
            IHttpRequestFactory httpRequestFactory,
            string baseAddress
        ) => (this.logger, this.httpRequestFactory, this.baseAddress) = (logger, httpRequestFactory, baseAddress);

        public Dictionary<string, string> Headers { get; } = new Dictionary<string, string>();

        public async Task<ListStateProvincesResponse> ListStateProvincesAsync(ListStateProvincesRequest request)
        {
            logger.LogInformation("GET: ListStateProvinces request to SalesPerson API");
            string uri = $"{baseAddress}";
            if (!string.IsNullOrEmpty(request.CountryRegionCode))
                uri += $"/?countryRegionCode={request.CountryRegionCode}";

            var response = await httpRequestFactory.Get(uri, Headers);
            logger.LogInformation($"GET: Response = {response.StatusCode} ({response.ReasonPhrase})");

            if (response.IsSuccessStatusCode)
            {
                return response.ContentAsType<ListStateProvincesResponse>();
            }

            return null;
        }
    }
}