using AutoMapper;
using AW.Core.Application.AutoMapper;
using ListTerritories = AW.Core.Abstractions.Api.SalesTerritoryApi.ListTerritories;

namespace AW.UI.Web.Internal.ViewModels.SalesTerritory
{
    public class SalesTerritoryViewModel : IMapFrom<ListTerritories.Territory>
    {
        public string Name { get; set; }

        public CountryRegionViewModel CountryRegion { get; set; }

        public string Group { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ListTerritories.Territory, SalesTerritoryViewModel>();
        }
    }
}