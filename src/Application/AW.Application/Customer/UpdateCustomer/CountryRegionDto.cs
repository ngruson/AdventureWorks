using AutoMapper;
using AW.Application.AutoMapper;

namespace AW.Application.Customer.UpdateCustomer
{
    public class CountryRegionDto : IMapFrom<Domain.Person.CountryRegion>
    {
        public string CountryRegionCode { get; set; }
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Person.CountryRegion, CountryRegionDto>();
        }
    }
}