using AutoMapper;
using AW.Core.Application.AutoMapper;
using AW.Infrastructure.Api.WCF.SalesTerritoryService;

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