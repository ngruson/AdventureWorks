using AutoMapper;
using AW.Common.AutoMapper;
using AW.Services.Customer.Domain;

namespace AW.Services.Customer.Application.GetCustomers
{
    public class CustomerAddressDto : IMapFrom<CustomerAddress>
    {
        public string AddressType { get; set; }
        public AddressDto Address { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CustomerAddress, CustomerAddressDto>();
        }
    }
}