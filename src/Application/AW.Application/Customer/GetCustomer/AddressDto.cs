using AutoMapper;
using AW.Application.AutoMapper;
using AW.Domain.Person;

namespace AW.Application.Customer.GetCustomer
{
    public class AddressDto : IMapFrom<Address>
    {
        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string PostalCode { get; set; }

        public string City { get; set; }

        public StateProvinceDto StateProvince { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Address, AddressDto>();
        }
    }
}