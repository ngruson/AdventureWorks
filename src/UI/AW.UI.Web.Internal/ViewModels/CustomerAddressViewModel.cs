using AutoMapper;
using AW.Application.AutoMapper;
using AW.UI.Web.Internal.CustomerService;

namespace AW.UI.Web.Internal.ViewModels
{
    public class CustomerAddressViewModel : IMapFrom<CustomerAddress>
    {
        public string AddressTypeName { get; set; }
        public AddressViewModel Address { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CustomerAddress, CustomerAddressViewModel>();
            profile.CreateMap<CustomerAddress1, CustomerAddressViewModel>();
        }
    }
}