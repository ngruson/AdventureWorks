using AutoMapper;
using AW.Common.AutoMapper;

namespace AW.Services.ReferenceData.Application.CountryRegion.GetCountries
{
    public class Country : IMapFrom<Domain.CountryRegion>
    {
        public string CountryRegionCode { get; set; }
        public string Name { get; set; }

        #if NETSTANDARD2_0
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.CountryRegion, Country>();
        }
        #endif
    }
}