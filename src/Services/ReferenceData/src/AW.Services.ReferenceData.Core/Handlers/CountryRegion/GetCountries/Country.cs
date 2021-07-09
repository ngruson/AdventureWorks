using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.ReferenceData.Core.Handlers.CountryRegion.GetCountries
{
    public class Country : IMapFrom<Core.Entities.CountryRegion>
    {
        public string CountryRegionCode { get; set; }
        public string Name { get; set; }

        #if NETSTANDARD2_0
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Core.Entities.CountryRegion, Country>();
        }
        #endif
    }
}