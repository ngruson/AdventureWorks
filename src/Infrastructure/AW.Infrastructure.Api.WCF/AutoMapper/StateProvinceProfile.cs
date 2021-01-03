using AutoMapper;
using ListStateProvinces = AW.Core.Abstractions.Api.StateProvinceApi.ListStateProvinces;

namespace AW.Infrastructure.Api.WCF.AutoMapper
{
    public class StateProvinceProfile : Profile
    {
        public StateProvinceProfile()
        {
            //Mappings for ListStateProvinces
            CreateMap<ListStateProvinces.ListStateProvincesRequest, StateProvinceService.ListStateProvincesRequest>();
            CreateMap<StateProvinceService.ListStateProvincesResponse, ListStateProvinces.ListStateProvincesResponse>();
            CreateMap<StateProvinceService.StateProvince, ListStateProvinces.StateProvince>();
        }
    }
}