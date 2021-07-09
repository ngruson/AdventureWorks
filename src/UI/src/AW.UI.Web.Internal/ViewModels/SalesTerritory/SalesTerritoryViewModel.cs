using AW.SharedKernel.AutoMapper;
using AW.UI.Web.Infrastructure.ApiClients.ReferenceDataApi.Models.GetTerritories;

namespace AW.UI.Web.Internal.ViewModels.SalesTerritory
{
    public class SalesTerritoryViewModel : IMapFrom<Territory>
    {
        public string Name { get; set; }

        public string CountryRegionCode { get; set; }

        public string Group { get; set; }
    }
}