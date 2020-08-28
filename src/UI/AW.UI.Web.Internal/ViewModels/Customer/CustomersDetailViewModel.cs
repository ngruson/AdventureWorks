using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace AW.UI.Web.Internal.ViewModels.Customer
{
    public class CustomerDetailViewModel
    {
        public CustomerViewModel Customer { get; set; }
        public List<SelectListItem> Territories { get; set; }
        public string TerritoryFilterApplied { get; set; }
    }
}