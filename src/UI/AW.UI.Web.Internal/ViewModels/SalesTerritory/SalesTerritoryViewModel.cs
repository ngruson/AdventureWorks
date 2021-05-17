using AW.UI.Web.Common.ApiClients.ReferenceDataApi.Models.GetTerritories;
using AW.Common.AutoMapper;

namespace AW.UI.Web.Internal.ViewModels.SalesTerritory
{
    public class SalesTerritoryViewModel : IMapFrom<Territory>
    {
        public string Name { get; set; }

        public string CountryRegionCode { get; set; }

        public string Group { get; set; }
    }
}