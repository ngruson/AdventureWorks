using AW.UI.Web.Internal.ApiClients.ReferenceDataApi.Models.GetAddressTypes;
using AW.UI.Web.Internal.ApiClients.ReferenceDataApi.Models.GetContactTypes;
using AW.UI.Web.Internal.ApiClients.ReferenceDataApi.Models.GetCountries;
using AW.UI.Web.Internal.ApiClients.ReferenceDataApi.Models.GetStateProvinces;
using AW.UI.Web.Internal.ApiClients.ReferenceDataApi.Models.GetTerritories;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AW.UI.Web.Internal.ApiClients.ReferenceDataApi
{
    public class ReferenceDataApiClient : IReferenceDataApiClient
    {
        private readonly HttpClient client;
        private readonly ILogger<ReferenceDataApiClient> logger;

        public ReferenceDataApiClient(HttpClient client, ILogger<ReferenceDataApiClient> logger) =>
            (this.client, this.logger) = (client, logger);

        public async Task<List<AddressType>> GetAddressTypesAsync()
        {
            string requestUri = $"/AddressType?api-version=1.0";
            logger.LogInformation("Getting address types");

            logger.LogInformation("Calling GET operation to {RequestUri}", client.BaseAddress + requestUri);
            using var response = await client.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();

            return await stream.DeserializeAsync<List<AddressType>>();
        }

        public async Task<List<ContactType>> GetContactTypesAsync()
        {
            string requestUri = $"/ContactType?api-version=1.0";
            logger.LogInformation("Getting contact types");

            logger.LogInformation("Calling GET operation to {RequestUri}", client.BaseAddress + requestUri);
            using var response = await client.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();

            return await stream.DeserializeAsync<List<ContactType>>();
        }

        public async Task<List<CountryRegion>> GetCountriesAsync()
        {
            string requestUri = $"/CountryRegion?api-version=1.0";
            logger.LogInformation("Getting countries");

            logger.LogInformation("Calling GET operation to {RequestUri}", client.BaseAddress + requestUri);
            using var response = await client.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();

            return await stream.DeserializeAsync<List<CountryRegion>>();
        }

        public async Task<List<StateProvince>> GetStateProvincesAsync(string countryRegionCode = null)
        {
            string requestUri = $"/StateProvince?api-version=1.0";
            logger.LogInformation("Getting states/provinces");

            if (!string.IsNullOrEmpty(countryRegionCode))
                requestUri += $"&countryRegionCode={countryRegionCode}";

            logger.LogInformation("Calling GET operation to {RequestUri}", client.BaseAddress + requestUri);
            using var response = await client.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();

            return await stream.DeserializeAsync<List<StateProvince>>();
        }

        public async Task<List<Territory>> GetTerritoriesAsync()
        {
            string requestUri = $"/Territory?api-version=1.0";
            logger.LogInformation("Getting territories");

            logger.LogInformation("Calling GET operation to {RequestUri}", client.BaseAddress + requestUri);
            using var response = await client.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();

            return await stream.DeserializeAsync<List<Territory>>();
        }
    }
}