using AutoMapper;
using AW.Core.Application.AutoMapper;
using GetCustomer = AW.Core.Abstractions.Api.CustomerApi.GetCustomer;
using AddCustomerAddress = AW.Core.Abstractions.Api.CustomerApi.AddCustomerAddress;
using UpdateCustomerAddress = AW.Core.Abstractions.Api.CustomerApi.UpdateCustomerAddress;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace AW.UI.Web.Internal.ViewModels.Customer
{
    public class EditCustomerAddressViewModel : IMapFrom<GetCustomer.CustomerAddress>
    {
        public bool IsNewAddress { get; set; }
        public string AccountNumber { get; set; }
        public string CustomerName { get; set; }
        public CustomerAddressViewModel CustomerAddress { get; set; }
        public IEnumerable<SelectListItem> AddressTypes { get; set; }
        public IEnumerable<SelectListItem> Countries { get; set; }
        public IEnumerable<SelectListItem> StateProvinces { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<EditCustomerAddressViewModel, GetCustomer.CustomerAddress>()
                .ForMember(m => m.AddressType, opt => opt.MapFrom(src => src.CustomerAddress.AddressType))
                .ForMember(m => m.Address, opt => opt.MapFrom(src => src.CustomerAddress.Address));
            profile.CreateMap<EditCustomerAddressViewModel, AddCustomerAddress.AddCustomerAddressRequest>()
                .ForMember(m => m.CustomerAddress, opt => opt.MapFrom(src => src.CustomerAddress));
            profile.CreateMap<EditCustomerAddressViewModel, UpdateCustomerAddress.UpdateCustomerAddressRequest>()
                .ForMember(m => m.CustomerAddress, opt => opt.MapFrom(src => src.CustomerAddress));
        }
    }
}