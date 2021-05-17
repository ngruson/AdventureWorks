using AutoMapper;
using AW.Common.AutoMapper;
using s = AW.UI.Web.Common.ApiClients.SalesOrderApi.Models;

namespace AW.UI.Web.Internal.ViewModels.SalesOrder
{
    public class AddressViewModel : IMapFrom<s.Address>
    {
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string StateProvinceCode { get; set; }
        public string CountryRegionCode { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<s.Address, AddressViewModel>()
                .ForMember(m => m.AddressLine2, opt => opt.MapFrom(src =>
                    !string.IsNullOrEmpty(src.AddressLine2) ? src.AddressLine2 : "-"));
        }
    }
}