using AutoMapper;
using AW.Application.AutoMapper;
using AW.Domain.Person;
using System;

namespace AW.Application.Customer.AddCustomerAddress
{
    public class CustomerAddressDto : IMapFrom<BusinessEntityAddress>
    {
        public AddressDto Address { get; set; }
        public string AddressTypeName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CustomerAddressDto, BusinessEntityAddress>()
                .ForMember(m => m.ModifiedDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(m => m.rowguid, opt => opt.MapFrom(src => Guid.NewGuid()))
            .ReverseMap();
        }
    }
}