using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.ReferenceData.Core.Handlers.StateProvince.GetStatesProvinces
{
    public class StateProvince : IMapFrom<Entities.StateProvince>
    {
        public string? StateProvinceCode { get; set; }

        public string? CountryRegionCode { get; set; }

        public bool IsOnlyStateProvinceFlag { get; set; }

        public string? Name { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Entities.StateProvince, StateProvince>()
                .ForMember(m => m.StateProvinceCode, opt => opt.MapFrom(src => src.StateProvinceCode!.Trim()))
                .ForMember(m => m.CountryRegionCode, opt => opt.MapFrom(src => src.CountryRegion!.CountryRegionCode));
        }
    }
}