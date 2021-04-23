using AutoMapper;
using AW.Services.ReferenceData.Application.Common;

namespace AW.Services.ReferenceData.Application.Territory.GetTerritories
{
    public class Territory : IMapFrom<Domain.Territory>
    {
        public string Name { get; set; }
        public string CountryRegionCode { get; set; }
        public string Group { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Territory, Territory>();
        }
    }
}