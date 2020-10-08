using AutoMapper;
using AW.Application.AutoMapper;

namespace AW.Application.StateProvince
{
    public class StateProvinceDto : IMapFrom<Domain.Person.StateProvince>
    {
        public string CountryRegionCode { get; set; }
        public string StateProvinceCode { get; set; }
        public string Name { get; set; }
        public string TerritoryName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Person.StateProvince, StateProvinceDto>();
        }
    }
}