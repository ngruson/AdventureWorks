using AutoMapper;
using AW.Core.Application.AutoMapper;
using GetCustomer = AW.Core.Abstractions.Api.CustomerApi.GetCustomer;
using ListCustomers = AW.Core.Abstractions.Api.CustomerApi.ListCustomers;
using UpdateCustomer = AW.Core.Abstractions.Api.CustomerApi.UpdateCustomer;
using AddCustomerAddress = AW.Core.Abstractions.Api.CustomerApi.AddCustomerAddress;
using UpdateCustomerAddress = AW.Core.Abstractions.Api.CustomerApi.UpdateCustomerAddress;
using System.ComponentModel.DataAnnotations;

namespace AW.UI.Web.Internal.ViewModels.Customer
{
    public class CustomerAddressViewModel : IMapFrom<GetCustomer.CustomerAddress>
    {
        [Display(Name = "Address type")]
        [Required]
        public string AddressType { get; set; }
        public AddressViewModel Address { get; set; } = new AddressViewModel();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<GetCustomer.CustomerAddress, CustomerAddressViewModel>();
            profile.CreateMap<ListCustomers.CustomerAddress, CustomerAddressViewModel>();

            profile.CreateMap<CustomerAddressViewModel, UpdateCustomer.CustomerAddress>();
            profile.CreateMap<CustomerAddressViewModel, AddCustomerAddress.CustomerAddress>();
            profile.CreateMap<CustomerAddressViewModel, UpdateCustomerAddress.CustomerAddress>();
        }
    }
}