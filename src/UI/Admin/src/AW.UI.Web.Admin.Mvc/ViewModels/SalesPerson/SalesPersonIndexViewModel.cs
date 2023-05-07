using AW.UI.Web.Infrastructure.Api.ReferenceData.Handlers.GetTerritories;

namespace AW.UI.Web.Admin.Mvc.ViewModels.SalesPerson
{
    public class SalesPersonIndexViewModel
    {
        public IEnumerable<Infrastructure.Api.SalesPerson.Handlers.GetSalesPersons.SalesPerson>? SalesPersons { get; set; }
        public IEnumerable<Territory>? Territories { get; set; }
    }
}