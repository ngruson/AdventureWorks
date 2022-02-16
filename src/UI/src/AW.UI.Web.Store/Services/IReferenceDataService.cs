using AW.UI.Web.Infrastructure.ApiClients.ReferenceDataApi.Models.GetCountries;
using AW.UI.Web.Infrastructure.ApiClients.ReferenceDataApi.Models.GetShipMethods;
using AW.UI.Web.Infrastructure.ApiClients.ReferenceDataApi.Models.GetStateProvinces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AW.UI.Web.Store.Services
{
    public interface IReferenceDataService
    {
        Task<List<CountryRegion>> GetCountriesAsync();
        Task<List<StateProvince>> GetStatesProvincesAsync(string countryRegionCode);
        Task<List<ShipMethod>> GetShipMethodsAsync();
    }
}