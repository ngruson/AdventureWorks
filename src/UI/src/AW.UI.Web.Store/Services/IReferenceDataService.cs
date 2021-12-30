using AW.UI.Web.Infrastructure.ApiClients.ReferenceDataApi.Models.GetCountries;
using AW.UI.Web.Infrastructure.ApiClients.ReferenceDataApi.Models.GetShipMethods;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AW.UI.Web.Store.Services
{
    public interface IReferenceDataService
    {
        Task<List<CountryRegion>> GetCountriesAsync();
        Task<List<ShipMethod>> GetShipMethodsAsync();
    }
}