using Ardalis.GuardClauses;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using AW.UI.Web.SharedKernel.ReferenceData.Handlers.GetCountries;
using AW.UI.Web.SharedKernel.ReferenceData.Handlers.GetShipMethods;
using AW.UI.Web.SharedKernel.ReferenceData.Handlers.GetStatesProvinces;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AW.UI.Web.Store.Services
{
    public class ReferenceDataService : IReferenceDataService
    {
        private readonly ILogger<ReferenceDataService> logger;
        private readonly IReferenceDataApiClient referenceDataApiClient;

        public ReferenceDataService(
            ILogger<ReferenceDataService> logger,
            IReferenceDataApiClient referenceDataApiClient
        ) => (this.logger, this.referenceDataApiClient) = (logger, referenceDataApiClient);

        public async Task<List<CountryRegion>> GetCountriesAsync()
        {
            logger.LogInformation("GetCountriesAsync called");

            logger.LogInformation("Getting countries from Reference Data API");
            var countries = await referenceDataApiClient.GetCountriesAsync();
            Guard.Against.Null(countries, nameof(countries));

            logger.LogInformation("Returning countries from Reference Data API");
            return countries;
        }

        public async Task<List<ShipMethod>> GetShipMethodsAsync()
        {
            logger.LogInformation("GetShipMethodsAsync called");

            logger.LogInformation("Getting shipping methods from Reference Data API");
            var shipMethods = await referenceDataApiClient.GetShipMethodsAsync();
            Guard.Against.Null(shipMethods, nameof(shipMethods));

            logger.LogInformation("Returning shipping methods from Reference Data API");
            return shipMethods;
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
    }
}