using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.ReferenceData.Core.Handlers.CountryRegion.GetCountries
{
    public class Country : IMapFrom<Core.Entities.CountryRegion>
    {
        public string? CountryRegionCode { get; private init; }
        public string? Name { get; private init; }

        #if NETSTANDARD2_0
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Core.Entities.CountryRegion, Country>();
        }
        #endif
    }
}