using AW.UI.Web.Internal.ApiClients.ReferenceDataApi.Models.GetAddressTypes;
using AW.UI.Web.Internal.ApiClients.ReferenceDataApi.Models.GetContactTypes;
using AW.UI.Web.Internal.ApiClients.ReferenceDataApi.Models.GetCountries;
using AW.UI.Web.Internal.ApiClients.ReferenceDataApi.Models.GetStateProvinces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AW.UI.Web.Internal.Services
{
    public interface IReferenceDataService
    {
        Task<List<AddressType>> GetAddressTypes();
        Task<List<ContactType>> GetContactTypes();
        Task<List<CountryRegion>> GetCountries();
        Task<List<StateProvince>> GetStatesProvinces(string countryRegionCode = null);
    }
}