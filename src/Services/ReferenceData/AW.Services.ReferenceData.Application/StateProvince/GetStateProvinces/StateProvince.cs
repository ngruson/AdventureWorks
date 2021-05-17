using AutoMapper;
using AW.Common.AutoMapper;

namespace AW.Services.ReferenceData.Application.StateProvince.GetStateProvinces
{
    public class StateProvince : IMapFrom<Domain.StateProvince>
    {
        public string StateProvinceCode { get; set; }

        public string CountryRegionCode { get; set; }

        public bool IsOnlyStateProvinceFlag { get; set; }

        public string Name { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.StateProvince, StateProvince>()
                .ForMember(m => m.StateProvinceCode, opt => opt.MapFrom(src => src.StateProvinceCode.Trim()));
        }
    }
}