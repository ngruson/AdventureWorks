using AW.UI.Web.Infrastructure.ApiClients.ReferenceDataApi.Models.GetAddressTypes;
using AW.UI.Web.Infrastructure.ApiClients.ReferenceDataApi.Models.GetContactTypes;
using AW.UI.Web.Infrastructure.ApiClients.ReferenceDataApi.Models.GetCountries;
using AW.UI.Web.Infrastructure.ApiClients.ReferenceDataApi.Models.GetShipMethods;
using AW.UI.Web.Infrastructure.ApiClients.ReferenceDataApi.Models.GetStateProvinces;
using AW.UI.Web.Infrastructure.ApiClients.ReferenceDataApi.Models.GetTerritories;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AW.UI.Web.Infrastructure.ApiClients.ReferenceDataApi
{
    public class ReferenceDataApiClient : IReferenceDataApiClient
    {
        private readonly HttpClient client;
        private readonly ILogger<ReferenceDataApiClient> logger;

        public ReferenceDataApiClient(HttpClient client, ILogger<ReferenceDataApiClient> logger) =>
            (this.client, this.logger) = (client, logger);

        public async Task<List<AddressType>> GetAddressTypesAsync()
        {
            string requestUri = $"referencedata-api/AddressType?api-version=1.0";
            logger.LogInformation("Getting address types");

            logger.LogInformation("Calling GET operation to {RequestUri}", client.BaseAddress + requestUri);
            using var response = await client.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();

            return await stream.DeserializeAsync<List<AddressType>>(new JsonSerializerOptions
            {
                Converters =
                {
                    new JsonStringEnumConverter()
                },
                IgnoreReadOnlyProperties = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }

        public async Task<List<ContactType>> GetContactTypesAsync()
        {
            string requestUri = "referencedata-api/ContactType?api-version=1.0";
            logger.LogInformation("Getting contact types");

            logger.LogInformation("Calling GET operation to {RequestUri}", client.BaseAddress + requestUri);
            using var response = await client.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();

            return await stream.DeserializeAsync<List<ContactType>>(new JsonSerializerOptions
            {
                Converters =
                {
                    new JsonStringEnumConverter()
                },
                IgnoreReadOnlyProperties = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }

        public async Task<List<CountryRegion>> GetCountriesAsync()
        {
            string requestUri = "referencedata-api/CountryRegion?api-version=1.0";
            logger.LogInformation("Getting countries");

            logger.LogInformation("Calling GET operation to {RequestUri}", client.BaseAddress + requestUri);
            using var response = await client.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();

            return await stream.DeserializeAsync<List<CountryRegion>>(new JsonSerializerOptions
            {
                Converters =
                {
                    new JsonStringEnumConverter()
                },
                IgnoreReadOnlyProperties = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }

        public async Task<List<ShipMethod>> GetShipMethodsAsync()
        {
            string requestUri = "referencedata-api/ShipMethod?api-version=1.0";
            logger.LogInformation("Getting shipping methods");

            logger.LogInformation("Calling GET operation to {RequestUri}", client.BaseAddress + requestUri);
            using var response = await client.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();

            return await stream.DeserializeAsync<List<ShipMethod>>(new JsonSerializerOptions
            {
                Converters =
                {
                    new JsonStringEnumConverter()
                },
                IgnoreReadOnlyProperties = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }

        public async Task<List<StateProvince>> GetStatesProvincesAsync(string countryRegionCode = null)
        {
            string requestUri = "referencedata-api/StateProvince?api-version=1.0";
            logger.LogInformation("Getting states/provinces");

            if (!string.IsNullOrEmpty(countryRegionCode))
                requestUri += $"&countryRegionCode={countryRegionCode}";

            logger.LogInformation("Calling GET operation to {RequestUri}", client.BaseAddress + requestUri);
            using var response = await client.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();

            return await stream.DeserializeAsync<List<StateProvince>>(new JsonSerializerOptions
            {
                Converters =
                {
                    new JsonStringEnumConverter()
                },
                IgnoreReadOnlyProperties = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }

        public async Task<List<Territory>> GetTerritoriesAsync()
        {
            string requestUri = "referencedata-api/Territory?api-version=1.0";
            logger.LogInformation("Getting territories");

            logger.LogInformation("Calling GET operation to {RequestUri}", client.BaseAddress + requestUri);
            using var response = await client.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();

            return await stream.DeserializeAsync<List<Territory>>(new JsonSerializerOptions
            {
                Converters =
                {
                    new JsonStringEnumConverter()
                },
                IgnoreReadOnlyProperties = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }
    }
}