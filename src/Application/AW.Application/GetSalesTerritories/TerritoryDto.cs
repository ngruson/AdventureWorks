using AutoMapper;
using AW.Application.Common.Mappings;
using AW.Domain.Sales;

namespace AW.Application.GetSalesTerritories
{
    public class TerritoryDto : IMapFrom<SalesTerritory>
    {
        public string Name { get; set; }
        public string CountryRegionCode { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SalesTerritory, TerritoryDto>();
        }
    }
}