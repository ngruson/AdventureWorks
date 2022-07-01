using AW.UI.Web.SharedKernel.ReferenceData.Handlers.GetTerritories;
using System.Collections.Generic;

namespace AW.UI.Web.Internal.ViewModels.SalesTerritory
{
    public class SalesTerritoryIndexViewModel
    {
        public IEnumerable<Territory> SalesTerritories { get; set; }
    }
}