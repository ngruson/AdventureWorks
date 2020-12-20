using AutoMapper;
using GetCustomer = AW.Core.Abstractions.Api.CustomerApi.GetCustomer;
using ListCustomers = AW.Core.Abstractions.Api.CustomerApi.ListCustomers;
using UpdateCustomer = AW.Core.Abstractions.Api.CustomerApi.UpdateCustomer;
using AddCustomerAddress = AW.Core.Abstractions.Api.CustomerApi.AddCustomerAddress;
using UpdateCustomerAddress = AW.Core.Abstractions.Api.CustomerApi.UpdateCustomerAddress;
using AW.Core.Application.AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace AW.UI.Web.Internal.ViewModels.Customer
{
    public class AddressViewModel : IMapFrom<ListCustomers.Address>
    {
        [Display(Name = "Address line 1")]
        [Required]
        public string AddressLine1 { get; set; }
        [Display(Name = "Address line 2")]
        public string AddressLine2 { get; set; }
        [Display(Name = "Postal code")]
        [Required]
        public string PostalCode { get; set; }
        [Display(Name = "City")]
        [Required]
        public string City { get; set; }
        public StateProvinceViewModel StateProvince { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ListCustomers.Address, AddressViewModel>();
            profile.CreateMap<GetCustomer.Address, AddressViewModel>()
                .ReverseMap();

            profile.CreateMap<AddressViewModel, UpdateCustomer.Address>();
            profile.CreateMap<AddressViewModel, AddCustomerAddress.Address>()
                .ForMember(m => m.StateProvinceCode, opt => opt.MapFrom(src => src.StateProvince.StateProvinceCode));
            profile.CreateMap<AddressViewModel, UpdateCustomerAddress.Address>()
                .ForMember(m => m.StateProvinceCode, opt => opt.MapFrom(src => src.StateProvince.StateProvinceCode));
        }
    }
}