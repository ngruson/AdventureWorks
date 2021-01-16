using AW.Core.Abstractions.Api.CountryApi;
using AW.Core.Abstractions.Api.CountryApi.ListCountries;
using AW.Infrastructure.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AW.Infrastructure.Api.REST
{
    public class CountryApi : ICountryApi
    {
        private ILogger<CountryApi> logger;
        private IHttpRequestFactory httpRequestFactory;
        private string baseAddress;

        public CountryApi(
            ILogger<CountryApi> logger,
            IHttpRequestFactory httpRequestFactory,
            string baseAddress
        ) => (this.logger, this.httpRequestFactory, this.baseAddress) = (logger, httpRequestFactory, baseAddress);

        public Dictionary<string, string> Headers { get; } = new Dictionary<string, string>();

        public async Task<ListCountriesResponse> ListCountriesAsync()
        {
            logger.LogInformation("GET: ListCountries request to Country API");
            var response = await httpRequestFactory.Get($"{baseAddress}", Headers);
            logger.LogInformation($"GET: Response = {response.StatusCode} ({response.ReasonPhrase})");

            if (response.IsSuccessStatusCode)
            {
                return response.ContentAsType<ListCountriesResponse>();
            }

            return null;
        }
    }
}