using AW.SharedKernel.AutoMapper;
using AW.UI.Web.Infrastructure.ApiClients.ReferenceDataApi.Models.GetStateProvinces;

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