using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.ReferenceData.Core.Handlers.Territory.GetTerritories
{
    public class Territory : IMapFrom<Entities.Territory>
    {
        public string Name { get; set; }
        public string CountryRegionCode { get; set; }
        public string Group { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Core.Entities.Territory, Territory>();
        }
    }
}