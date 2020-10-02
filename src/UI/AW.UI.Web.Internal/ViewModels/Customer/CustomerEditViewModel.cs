using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace AW.UI.Web.Internal.ViewModels.Customer
{
    public class CustomerEditViewModel
    {
        public CustomerViewModel Customer { get; set; }
        public IEnumerable<SelectListItem> Territories { get; set; }
        public IEnumerable<SelectListItem> SalesPersons { get; set; }
    }
}