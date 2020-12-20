using AutoMapper;
using AW.Core.Application.AutoMapper;

namespace AW.Core.Application.Customer.GetCustomer
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