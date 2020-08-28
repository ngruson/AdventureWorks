using AutoMapper;
using AW.Application.AutoMapper;
using AW.UI.Web.Internal.SalesOrderService;

namespace AW.UI.Web.Internal.ViewModels.SalesOrder
{
    public class AddressViewModel : IMapFrom<Address>
    {
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string StateProvinceName { get; set; }
        public string Country { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Address, AddressViewModel>()
                .ForMember(m => m.AddressLine2, opt => opt.MapFrom(src =>
                    !string.IsNullOrEmpty(src.AddressLine2) ? src.AddressLine2 : "-"));

            profile.CreateMap<Address1, AddressViewModel>()
                .ForMember(m => m.AddressLine2, opt => opt.MapFrom(src =>
                    !string.IsNullOrEmpty(src.AddressLine2) ? src.AddressLine2 : "-"));
        }
    }
}