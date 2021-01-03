using AutoMapper;
using ListTerritories = AW.Core.Abstractions.Api.SalesTerritoryApi.ListTerritories;

namespace AW.Infrastructure.Api.WCF.AutoMapper
{
    public class SalesTerritoryProfile : Profile
    {
        public SalesTerritoryProfile()
        {
            //Mappings for ListSalesOrders
            CreateMap<SalesTerritoryService.ListTerritoriesResponse, ListTerritories.ListTerritoriesResponse>()
                .ForMember(m => m.Territories, opt => opt.MapFrom(src => src.ListTerritoriesResult));
            CreateMap<SalesTerritoryService.TerritoryDto, ListTerritories.Territory>();
            CreateMap<SalesTerritoryService.CountryRegionDto, ListTerritories.CountryRegion>();
        }
    }
}