using Ardalis.GuardClauses;
using AW.UI.Web.Infrastructure.ApiClients.ReferenceDataApi;
using AW.UI.Web.Infrastructure.ApiClients.ReferenceDataApi.Models.GetAddressTypes;
using AW.UI.Web.Infrastructure.ApiClients.ReferenceDataApi.Models.GetContactTypes;
using AW.UI.Web.Infrastructure.ApiClients.ReferenceDataApi.Models.GetCountries;
using AW.UI.Web.Infrastructure.ApiClients.ReferenceDataApi.Models.GetStateProvinces;
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

        public async Task<List<AddressType>> GetAddressTypes()
        {
            logger.LogInformation("GetAddressTypes called");

            logger.LogInformation("Getting address types from Reference Data API");
            var addressTypes = await referenceDataApiClient.GetAddressTypesAsync();
            Guard.Against.Null(addressTypes, nameof(addressTypes));

            logger.LogInformation("Returning address types from Reference Data API");
            return addressTypes;
        }

        public async Task<List<ContactType>> GetContactTypes()
        {
            logger.LogInformation("GetContactTypes called");

            logger.LogInformation("Getting contact types from Reference Data API");
            var contactTypes = await referenceDataApiClient.GetContactTypesAsync();
            Guard.Against.Null(contactTypes, nameof(contactTypes));

            logger.LogInformation("Returning contact types from Reference Data API");
            return contactTypes;
        }

        public async Task<List<CountryRegion>> GetCountries()
        {
            logger.LogInformation("GetCountries called");

            logger.LogInformation("Getting countries from Reference Data API");
            var countries = await referenceDataApiClient.GetCountriesAsync();
            Guard.Against.Null(countries, nameof(countries));

            logger.LogInformation("Returning countries from Reference Data API");
            return countries;
        }

        public async Task<List<StateProvince>> GetStatesProvinces(string countryRegionCode = null)
        {
            logger.LogInformation("GetStateProvinces called");

            logger.LogInformation("Getting states/provinces from Reference Data API");
            var statesProvinces = await referenceDataApiClient.GetStateProvincesAsync(countryRegionCode);
            Guard.Against.Null(statesProvinces, nameof(statesProvinces));

            logger.LogInformation("Returning states/provinces from Reference Data API");
            return statesProvinces;
        }
    }
}