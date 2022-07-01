using AW.UI.Web.SharedKernel.ReferenceData.Handlers.GetAddressTypes;
using AW.UI.Web.SharedKernel.ReferenceData.Handlers.GetContactTypes;
using AW.UI.Web.SharedKernel.ReferenceData.Handlers.GetCountries;
using AW.UI.Web.SharedKernel.ReferenceData.Handlers.GetShipMethods;
using AW.UI.Web.SharedKernel.ReferenceData.Handlers.GetStatesProvinces;
using AW.UI.Web.SharedKernel.ReferenceData.Handlers.GetTerritories;

namespace AW.UI.Web.SharedKernel.Interfaces.Api
{
    public interface IReferenceDataApiClient
    {
        Task<List<AddressType>> GetAddressTypesAsync();
        Task<List<ContactType>> GetContactTypesAsync();
        Task<List<CountryRegion>> GetCountriesAsync();
        Task<List<ShipMethod>> GetShipMethodsAsync();
        Task<List<StateProvince>> GetStatesProvincesAsync(string? countryRegionCode = null);
        Task<List<Territory>> GetTerritoriesAsync(string? countryRegionCode = null);
    }
}