using AutoMapper;
using AW.Application.AutoMapper;
using AW.UI.Web.Internal.CustomerService;
using System.ComponentModel.DataAnnotations;

namespace AW.UI.Web.Internal.ViewModels.Customer
{
    public class CustomerAddressViewModel : IMapFrom<CustomerAddress>
    {
        [Display(Name = "Address type")]
        [Required]
        public string AddressType { get; set; }
        public AddressViewModel Address { get; set; } = new AddressViewModel();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CustomerAddress, CustomerAddressViewModel>();
            profile.CreateMap<CustomerAddress1, CustomerAddressViewModel>();
            profile.CreateMap<CustomerAddressViewModel, CustomerAddress3>();
        }
    }
}