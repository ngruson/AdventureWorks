using AW.UI.Web.Internal.ApiClients.ReferenceDataApi.Models.GetAddressTypes;
using AW.UI.Web.Internal.ApiClients.ReferenceDataApi.Models.GetContactTypes;
using AW.UI.Web.Internal.ApiClients.ReferenceDataApi.Models.GetCountries;
using AW.UI.Web.Internal.ApiClients.ReferenceDataApi.Models.GetStateProvinces;
using AW.UI.Web.Internal.ApiClients.ReferenceDataApi.Models.GetTerritories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AW.UI.Web.Internal.ApiClients.ReferenceDataApi
{
    public interface IReferenceDataApiClient
    {
        Task<List<AddressType>> GetAddressTypesAsync();
        Task<List<ContactType>> GetContactTypesAsync();
        Task<List<CountryRegion>> GetCountriesAsync();
        Task<List<StateProvince>> GetStateProvincesAsync(string countryRegionCode = null);
        Task<List<Territory>> GetTerritoriesAsync();
    }
}