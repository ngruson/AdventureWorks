using AutoMapper;
using AW.Application.AutoMapper;
using AW.Application.Customer;

namespace AW.CustomerService.Messages
{
    public class CustomerAddress : IMapFrom<CustomerAddressDto>
    {
        public Address Address { get; set; }
        public string AddressTypeName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CustomerAddressDto, CustomerAddress>();
        }
    }
}