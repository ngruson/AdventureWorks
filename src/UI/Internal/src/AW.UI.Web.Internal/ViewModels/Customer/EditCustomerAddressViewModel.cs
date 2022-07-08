using AutoMapper;
using AW.SharedKernel.AutoMapper;
using AW.UI.Web.SharedKernel.Customer.Handlers.GetIndividualCustomer;

namespace AW.UI.Web.Internal.ViewModels.Customer
{
    public class EditCustomerAddressViewModel : IMapFrom<CustomerAddress>
    {
        public bool IsNewAddress { get; set; }
        public string AccountNumber { get; set; }
        public string CustomerName { get; set; }
        public CustomerAddressViewModel CustomerAddress { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<EditCustomerAddressViewModel, CustomerAddress>()
                .ForMember(m => m.AddressType, opt => opt.MapFrom(src => src.CustomerAddress.AddressType))
                .ForMember(m => m.Address, opt => opt.MapFrom(src => src.CustomerAddress.Address));
            profile.CreateMap<EditCustomerAddressViewModel, SharedKernel.Customer.Handlers.UpdateCustomer.CustomerAddress>()
                .ForMember(m => m.AddressType, opt => opt.MapFrom(src => src.CustomerAddress.AddressType))
                .ForMember(m => m.Address, opt => opt.MapFrom(src => src.CustomerAddress.Address));
        }
    }
}