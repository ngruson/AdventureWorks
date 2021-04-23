using AW.UI.Web.Internal.Common;
using AW.UI.Web.Internal.ApiClients.ReferenceDataApi.Models.GetTerritories;

namespace AW.UI.Web.Internal.ViewModels.SalesTerritory
{
    public class SalesTerritoryViewModel : IMapFrom<Territory>
    {
        public string Name { get; set; }

        public string CountryRegionCode { get; set; }

        public string Group { get; set; }
    }
}