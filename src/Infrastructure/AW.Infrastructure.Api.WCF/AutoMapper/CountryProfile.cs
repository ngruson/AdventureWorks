using AutoMapper;
using AW.Core.Abstractions.Api.CountryApi.ListCountries;

namespace AW.Infrastructure.Api.WCF.AutoMapper
{
    public class CountryProfile : Profile
    {
        public CountryProfile()
        {
            //Mappings for ListCountries
            CreateMap<CountryService.ListCountriesResponse, ListCountriesResponse>();
            CreateMap<CountryService.CountryDto, Country>();

        }
    }
}