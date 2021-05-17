using AutoMapper;
using AW.Common.AutoMapper;

namespace AW.Services.Customer.Application.AddCustomerAddress
{
    public class CustomerAddressDto : IMapFrom<Domain.CustomerAddress>
    {
        public AddressDto Address { get; set; }
        public string AddressType { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CustomerAddressDto, Domain.CustomerAddress>()
                .ForMember(m => m.Id, opt => opt.Ignore())
                .ForMember(m => m.AddressID, opt => opt.Ignore());
        }
    }
}