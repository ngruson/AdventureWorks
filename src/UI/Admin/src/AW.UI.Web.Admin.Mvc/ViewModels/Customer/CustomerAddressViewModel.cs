using AutoMapper;
using AW.SharedKernel.AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace AW.UI.Web.Admin.Mvc.ViewModels.Customer
{
    public class CustomerAddressViewModel : IMapFrom<SharedKernel.Customer.Handlers.GetCustomers.CustomerAddress>
    {
        [Display(Name = "Address type")]
        [Required]
        public string? AddressType { get; set; }
        public AddressViewModel Address { get; set; } = new AddressViewModel();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SharedKernel.Customer.Handlers.GetCustomers.CustomerAddress, CustomerAddressViewModel>();
            profile.CreateMap<SharedKernel.Customer.Handlers.GetCustomer.CustomerAddress, CustomerAddressViewModel>();
            profile.CreateMap<SharedKernel.Customer.Handlers.GetIndividualCustomer.CustomerAddress, CustomerAddressViewModel>()
                .ReverseMap();
            profile.CreateMap<SharedKernel.Customer.Handlers.GetStoreCustomer.CustomerAddress, CustomerAddressViewModel>();
            profile.CreateMap<CustomerAddressViewModel, SharedKernel.Customer.Handlers.UpdateCustomer.CustomerAddress>();
        }
    }
}