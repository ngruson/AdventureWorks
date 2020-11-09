using AutoMapper;
using AW.Application.AutoMapper;

namespace AW.Application.Country.ListCountries
{
    public class CountryDto : IMapFrom<Domain.Person.StateProvince>
    {
        public string CountryRegionCode { get; set; }
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Person.StateProvince, CountryDto>();
        }
    }
}