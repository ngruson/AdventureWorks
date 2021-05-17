using AutoMapper;
using AW.Common.AutoMapper;
using m = AW.UI.Web.Common.ApiClients.CustomerApi.Models;

namespace AW.UI.Web.Internal.ViewModels.Customer
{
    public class EditCustomerAddressViewModel : IMapFrom<m.GetCustomer.CustomerAddress>
    {
        public bool IsNewAddress { get; set; }
        public string AccountNumber { get; set; }
        public string CustomerName { get; set; }
        public CustomerAddressViewModel CustomerAddress { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<EditCustomerAddressViewModel, m.GetCustomer.CustomerAddress>()
                .ForMember(m => m.AddressType, opt => opt.MapFrom(src => src.CustomerAddress.AddressType))
                .ForMember(m => m.Address, opt => opt.MapFrom(src => src.CustomerAddress.Address));
            profile.CreateMap<EditCustomerAddressViewModel, m.UpdateCustomer.CustomerAddress>()
                .ForMember(m => m.AddressType, opt => opt.MapFrom(src => src.CustomerAddress.AddressType))
                .ForMember(m => m.Address, opt => opt.MapFrom(src => src.CustomerAddress.Address));
        }
    }
}