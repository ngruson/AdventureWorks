using AutoMapper;
using AW.Application.AutoMapper;
using AW.Domain.Person;
using System;

namespace AW.Application.Customer.AddCustomerAddress
{
    public class AddressDto : IMapFrom<Address>
    {
        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string PostalCode { get; set; }

        public string City { get; set; }

        public string StateProvinceCode { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AddressDto, Address>()
                .ForMember(m => m.ModifiedDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(m => m.rowguid, opt => opt.MapFrom(src => Guid.NewGuid()));
        }
    }
}