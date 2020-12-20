using AutoMapper;
using AW.Core.Application.AutoMapper;
using AW.Core.Domain.Person;

namespace AW.Core.Application.Customer.GetCustomer
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