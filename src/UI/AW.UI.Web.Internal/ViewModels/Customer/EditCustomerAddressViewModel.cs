using AutoMapper;
using AW.Application.AutoMapper;
using AW.UI.Web.Internal.CustomerService;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace AW.UI.Web.Internal.ViewModels.Customer
{
    public class EditCustomerAddressViewModel : IMapFrom<CustomerAddress2>
    {
        public bool IsNewAddress { get; set; }
        public string AccountNumber { get; set; }
        public string CustomerName { get; set; }
        public CustomerAddressViewModel CustomerAddressViewModel { get; set; }
        public IEnumerable<SelectListItem> AddressTypes { get; set; }
        public IEnumerable<SelectListItem> Countries { get; set; }
        public IEnumerable<SelectListItem> StateProvinces { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<EditCustomerAddressViewModel, CustomerAddress2>();
            profile.CreateMap<EditCustomerAddressViewModel, UpdateCustomerAddressRequest>()
                .ForMember(m => m.CustomerAddress, opt => opt.MapFrom(src => src.CustomerAddressViewModel));
        }
    }
}