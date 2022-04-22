using AW.UI.Web.Infrastructure.ApiClients.ReferenceDataApi.Models.GetAddressTypes;
using AW.UI.Web.Infrastructure.ApiClients.ReferenceDataApi.Models.GetContactTypes;
using AW.UI.Web.Infrastructure.ApiClients.ReferenceDataApi.Models.GetCountries;
using AW.UI.Web.Infrastructure.ApiClients.ReferenceDataApi.Models.GetStateProvinces;
using AW.UI.Web.Infrastructure.ApiClients.ReferenceDataApi.Models.GetTerritories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AW.UI.Web.Internal.Services
{
    public interface IReferenceDataService
    {
        Task<List<AddressType>> GetAddressTypesAsync();
        Task<List<ContactType>> GetContactTypesAsync();
        Task<List<CountryRegion>> GetCountriesAsync();
        Task<List<StateProvince>> GetStatesProvincesAsync(string countryRegionCode = null);
        Task<List<Territory>> GetTerritoriesAsync(string countryRegionCode = null);
    }
}