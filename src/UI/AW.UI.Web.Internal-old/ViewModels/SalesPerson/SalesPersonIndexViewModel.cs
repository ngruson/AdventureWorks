using AW.UI.Web.Internal.ViewModels.SalesTerritory;
using System.Collections.Generic;

namespace AW.UI.Web.Internal.ViewModels.SalesPerson
{
    public class SalesPersonIndexViewModel
    {
        public IEnumerable<SalesPersonViewModel> SalesPersons { get; set; }
        public IEnumerable<SalesTerritoryViewModel> Territories { get; set; }
    }
}