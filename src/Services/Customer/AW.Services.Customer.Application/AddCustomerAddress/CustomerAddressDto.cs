using AutoMapper;
using AW.Services.Customer.Application.Common;

namespace AW.Services.Customer.Application.AddCustomerAddress
{
    public class CustomerAddressDto : IMapFrom<Domain.CustomerAddress>
    {
        public AddressDto Address { get; set; }
        public string AddressTypeName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CustomerAddressDto, Domain.CustomerAddress>();
        }
    }
}