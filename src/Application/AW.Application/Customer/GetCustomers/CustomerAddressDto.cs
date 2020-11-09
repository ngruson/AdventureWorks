using AutoMapper;
using AW.Application.AutoMapper;
using AW.Domain.Person;

namespace AW.Application.Customer.GetCustomers
{
    public class CustomerAddressDto : IMapFrom<BusinessEntityAddress>
    {
        public AddressDto Address { get; set; }
        public string AddressTypeName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<BusinessEntityAddress, CustomerAddressDto>();
        }
    }
}