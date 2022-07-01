using AW.UI.Web.SharedKernel.Interfaces.Api;
using AW.UI.Web.SharedKernel.ReferenceData.Handlers.GetAddressTypes;
using AW.UI.Web.SharedKernel.ReferenceData.Handlers.GetContactTypes;
using AW.UI.Web.SharedKernel.ReferenceData.Handlers.GetCountries;
using AW.UI.Web.SharedKernel.ReferenceData.Handlers.GetShipMethods;
using AW.UI.Web.SharedKernel.ReferenceData.Handlers.GetStatesProvinces;
using AW.UI.Web.SharedKernel.ReferenceData.Handlers.GetTerritories;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AW.UI.Web.Infrastructure.ApiClients
{
    public class ReferenceDataApiClient : IReferenceDataApiClient
    {
        private readonly HttpClient client;
        private readonly ILogger<ReferenceDataApiClient> logger;

        public ReferenceDataApiClient(HttpClient client, ILogger<ReferenceDataApiClient> logger) =>
            (this.client, this.logger) = (client, logger);

        public async Task<List<AddressType>> GetAddressTypesAsync()
        {
            string requestUri = "AddressType?api-version=1.0";
            logger.LogInformation("Getting address types");

            logger.LogInformation("Calling GET operation to {RequestUri}", $"{client.BaseAddress}/{requestUri}");
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
            string requestUri = "ContactType?api-version=1.0";
            logger.LogInformation("Getting contact types");

            logger.LogInformation("Calling GET operation to {RequestUri}", $"{client.BaseAddress}/{requestUri}");
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
            string requestUri = "CountryRegion?api-version=1.0";
            logger.LogInformation("Getting countries");

            logger.LogInformation("Calling GET operation to {RequestUri}", $"{client.BaseAddress}/{requestUri}");
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
            string requestUri = "ShipMethod?api-version=1.0";
            logger.LogInformation("Getting shipping methods");

            logger.LogInformation("Calling GET operation to {RequestUri}", $"{client.BaseAddress}/{requestUri}");
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
            string requestUri = "StateProvince?api-version=1.0";
            logger.LogInformation("Getting states/provinces");

            if (!string.IsNullOrEmpty(countryRegionCode))
                requestUri += $"&countryRegionCode={countryRegionCode}";

            logger.LogInformation("Calling GET operation to {RequestUri}", $"{client.BaseAddress}/{requestUri}");
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

        public async Task<List<Territory>> GetTerritoriesAsync(string countryRegionCode = null)
        {
            string requestUri = "Territory?api-version=1.0";
            logger.LogInformation("Getting territories");

            if (!string.IsNullOrEmpty(countryRegionCode))
                requestUri += $"&countryRegionCode={countryRegionCode}";

            logger.LogInformation("Calling GET operation to {RequestUri}", $"{client.BaseAddress}/{requestUri}");
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