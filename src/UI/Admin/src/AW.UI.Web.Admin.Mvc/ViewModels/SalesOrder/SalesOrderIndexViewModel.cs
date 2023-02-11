using Microsoft.AspNetCore.Mvc.Rendering;

namespace AW.UI.Web.Admin.Mvc.ViewModels.SalesOrder
{
    public class SalesOrderIndexViewModel
    {
        public SalesOrderIndexViewModel(List<SalesOrderViewModel> salesOrders, List<SelectListItem> territories, List<SelectListItem> customerTypes, string territoryFilterApplied, string customerTypeFilterApplied, PaginationInfoViewModel paginationInfo)
        {
            SalesOrders = salesOrders;
            Territories = territories;
            CustomerTypes = customerTypes;
            TerritoryFilterApplied = territoryFilterApplied;
            CustomerTypeFilterApplied = customerTypeFilterApplied;
            PaginationInfo = paginationInfo;
        }

        public List<SalesOrderViewModel> SalesOrders { get; set; }
        public List<SelectListItem> Territories { get; set; }
        public List<SelectListItem> CustomerTypes { get; set; }
        public string TerritoryFilterApplied { get; set; }
        public string CustomerTypeFilterApplied { get; set; }
        public PaginationInfoViewModel PaginationInfo { get; set; }
    }
}