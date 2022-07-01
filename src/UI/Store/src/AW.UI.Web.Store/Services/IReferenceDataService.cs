using AW.UI.Web.SharedKernel.ReferenceData.Handlers.GetCountries;
using AW.UI.Web.SharedKernel.ReferenceData.Handlers.GetShipMethods;
using AW.UI.Web.SharedKernel.ReferenceData.Handlers.GetStatesProvinces;
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