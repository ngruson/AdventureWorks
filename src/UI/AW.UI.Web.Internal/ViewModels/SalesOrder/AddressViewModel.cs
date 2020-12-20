using AutoMapper;
using AW.Core.Application.AutoMapper;
using ListCustomers = AW.Core.Abstractions.Api.CustomerApi.ListCustomers;
using GetCustomer = AW.Core.Abstractions.Api.CustomerApi.GetCustomer;

namespace AW.UI.Web.Internal.ViewModels.SalesOrder
{
    public class AddressViewModel : IMapFrom<ListCustomers.Address>
    {
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string StateProvinceName { get; set; }
        public string Country { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ListCustomers.Address, AddressViewModel>()
                .ForMember(m => m.AddressLine2, opt => opt.MapFrom(src =>
                    !string.IsNullOrEmpty(src.AddressLine2) ? src.AddressLine2 : "-"))
                .ForMember(m => m.Country, opt => opt.MapFrom(src => src.StateProvince.CountryRegion.CountryRegionCode));

            profile.CreateMap<GetCustomer.Address, AddressViewModel>()
                .ForMember(m => m.AddressLine2, opt => opt.MapFrom(src =>
                    !string.IsNullOrEmpty(src.AddressLine2) ? src.AddressLine2 : "-"))
                .ForMember(m => m.Country, opt => opt.MapFrom(src => src.StateProvince.CountryRegion.CountryRegionCode));

            profile.CreateMap<Infrastructure.Api.WCF.SalesOrderService.Address, AddressViewModel>();
            profile.CreateMap<Infrastructure.Api.WCF.SalesOrderService.Address1, AddressViewModel>();
        }
    }
}