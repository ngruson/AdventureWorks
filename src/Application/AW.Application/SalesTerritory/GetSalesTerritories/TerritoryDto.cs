using AutoMapper;
using AW.Application.AutoMapper;

namespace AW.Application.SalesTerritory.GetSalesTerritories
{
    public class TerritoryDto : IMapFrom<Domain.Sales.SalesTerritory>
    {
        public string Name { get; set; }
        public CountryRegionDto CountryRegion { get; set; }
        public string Group { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Sales.SalesTerritory, TerritoryDto>();
        }
    }
}