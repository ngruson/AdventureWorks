using AutoMapper;
using AW.Core.Application.AutoMapper;
using AW.Core.Application.StateProvince;

namespace AW.StateProvinceService.Messages.ListStateProvinces
{
    public class StateProvince : IMapFrom<StateProvinceDto>
    {
        public string CountryRegionCode { get; set; }
        public string StateProvinceCode { get; set; }
        public string Name { get; set; }
        public string TerritoryName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<StateProvinceDto, StateProvince>();
        }
    }
}