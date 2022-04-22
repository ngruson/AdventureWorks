using Ardalis.GuardClauses;
using AW.UI.Web.Infrastructure.ApiClients.ReferenceDataApi;
using AW.UI.Web.Infrastructure.ApiClients.ReferenceDataApi.Models.GetAddressTypes;
using AW.UI.Web.Infrastructure.ApiClients.ReferenceDataApi.Models.GetContactTypes;
using AW.UI.Web.Infrastructure.ApiClients.ReferenceDataApi.Models.GetCountries;
using AW.UI.Web.Infrastructure.ApiClients.ReferenceDataApi.Models.GetStateProvinces;
using AW.UI.Web.Infrastructure.ApiClients.ReferenceDataApi.Models.GetTerritories;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AW.UI.Web.Internal.Services
{
    public class ReferenceDataService : IReferenceDataService
    {
        private readonly ILogger<ReferenceDataService> logger;
        private readonly IReferenceDataApiClient referenceDataApiClient;

        public ReferenceDataService(
            ILogger<ReferenceDataService> logger,
            IReferenceDataApiClient referenceDataApiClient
        ) => (this.logger, this.referenceDataApiClient) = (logger, referenceDataApiClient);

        public async Task<List<AddressType>> GetAddressTypesAsync()
        {
            logger.LogInformation("GetAddressTypesAsync called");

            logger.LogInformation("Getting address types from Reference Data API");
            var addressTypes = await referenceDataApiClient.GetAddressTypesAsync();
            Guard.Against.Null(addressTypes, nameof(addressTypes));

            logger.LogInformation("Returning address types from Reference Data API");
            return addressTypes;
        }

        public async Task<List<ContactType>> GetContactTypesAsync()
        {
            logger.LogInformation("GetContactTypesAsync called");

            logger.LogInformation("Getting contact types from Reference Data API");
            var contactTypes = await referenceDataApiClient.GetContactTypesAsync();
            Guard.Against.Null(contactTypes, nameof(contactTypes));

            logger.LogInformation("Returning contact types from Reference Data API");
            return contactTypes;
        }

        public async Task<List<CountryRegion>> GetCountriesAsync()
        {
            logger.LogInformation("GetCountriesAsync called");

            logger.LogInformation("Getting countries from Reference Data API");
            var countries = await referenceDataApiClient.GetCountriesAsync();
            Guard.Against.Null(countries, nameof(countries));

            logger.LogInformation("Returning countries from Reference Data API");
            return countries;
        }

        public async Task<List<StateProvince>> GetStatesProvincesAsync(string countryRegionCode = null)
        {
            logger.LogInformation("GetStatesProvincesAsync called");

            logger.LogInformation("Getting states/provinces from Reference Data API");
            var statesProvinces = await referenceDataApiClient.GetStatesProvincesAsync(countryRegionCode);
            Guard.Against.Null(statesProvinces, nameof(statesProvinces));

            logger.LogInformation("Returning states/provinces from Reference Data API");
            return statesProvinces;
        }

        public async Task<List<Territory>> GetTerritoriesAsync(string countryRegionCode = null)
        {
            logger.LogInformation("GetTerritoriesAsync called");

            logger.LogInformation("Getting territories from Reference Data API");
            var territories = await referenceDataApiClient.GetTerritoriesAsync(countryRegionCode);
            Guard.Against.Null(territories, nameof(territories));

            logger.LogInformation("Returning territories from Reference Data API");
            return territories;
        }
    }
}