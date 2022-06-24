using AutoMapper;
using AW.SharedKernel.AutoMapper;
using System.ComponentModel.DataAnnotations;
using m = AW.UI.Web.Infrastructure.ApiClients.CustomerApi.Models;

namespace AW.UI.Web.Internal.ViewModels.Customer
{
    public class CustomerAddressViewModel : IMapFrom<m.GetCustomer.CustomerAddress>
    {
        [Display(Name = "Address type")]
        [Required]
        public string AddressType { get; set; }
        public AddressViewModel Address { get; set; } = new AddressViewModel();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<m.GetCustomer.CustomerAddress, CustomerAddressViewModel>();
            profile.CreateMap<m.GetCustomers.CustomerAddress, CustomerAddressViewModel>();
            profile.CreateMap<CustomerAddressViewModel, m.UpdateCustomer.CustomerAddress>();
        }
    }
}