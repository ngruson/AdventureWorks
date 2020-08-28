using AutoMapper;
using AW.Application.AutoMapper;
using AW.Domain.Person;

namespace AW.Application.SalesOrder
{
    public class AddressDto : IMapFrom<Address>
    {
        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string City { get; set; }

        public string StateProvinceName { get; set; }

        public string Country { get; set; }

        public string PostalCode { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Address, AddressDto>()
                .ForMember(m => m.Country, opt => opt.MapFrom(src => src.StateProvince.CountryRegion.Name));
        }
    }
}