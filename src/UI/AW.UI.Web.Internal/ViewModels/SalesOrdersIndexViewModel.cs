using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace AW.UI.Web.Internal.ViewModels
{
    public class SalesOrdersIndexViewModel
    {
        public List<SalesOrderViewModel> SalesOrders { get; set; }
        public List<SelectListItem> Territories { get; set; }
        public List<SelectListItem> CustomerTypes { get; set; }
        public string TerritoryFilterApplied { get; set; }
        public string CustomerTypeFilterApplied { get; set; }
        public PaginationInfoViewModel PaginationInfo { get; set; }
    }
}