using AutoMapper;
using AW.Core.Application.AutoMapper;
using AW.Core.Domain.Person;

namespace AW.Core.Application.Customer.UpdateCustomer
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