using AutoMapper;
using AW.Core.Application.AutoMapper;
using AW.Core.Domain.Person;

namespace AW.Core.Application.SalesOrder.GetSalesOrders
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