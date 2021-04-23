using AW.UI.Web.Internal.ApiClients.ReferenceDataApi.Models.GetStateProvinces;
using AW.UI.Web.Internal.Common;

namespace AW.UI.Web.Internal.ViewModels.Customer
{
    public class StateProvinceViewModel : IMapFrom<StateProvince>
    {
        public string StateProvinceCode { get; set; }
        public string CountryRegionCode { get; set; }
        public bool IsOnlyStateProvinceFlag { get; set; }
        public string Name { get; set; }
    }
}