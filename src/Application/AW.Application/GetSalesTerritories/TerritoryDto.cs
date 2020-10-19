using AutoMapper;
using AW.Application.AutoMapper;
using AW.Domain.Sales;

namespace AW.Application.GetSalesTerritories
{
    public class TerritoryDto : IMapFrom<SalesTerritory>
    {
        public string Name { get; set; }
        public CountryRegionDto CountryRegion { get; set; }
        public string Group { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SalesTerritory, TerritoryDto>();
        }
    }
}