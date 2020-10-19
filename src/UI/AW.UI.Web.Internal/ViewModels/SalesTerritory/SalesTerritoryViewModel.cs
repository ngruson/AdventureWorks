using AutoMapper;
using AW.Application.AutoMapper;
using AW.UI.Web.Internal.SalesTerritoryService;

namespace AW.UI.Web.Internal.ViewModels.SalesTerritory
{
    public class SalesTerritoryViewModel : IMapFrom<TerritoryDto>
    {
        public string Name { get; set; }

        public CountryRegionViewModel CountryRegion { get; set; }

        public string Group { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TerritoryDto, SalesTerritoryViewModel>();
        }
    }
}