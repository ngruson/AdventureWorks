using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AW.UI.Web.Internal.ViewModels.Customer
{
    public class EditCustomerContactViewModel
    {
        public bool IsNewContact { get; set; }
        public string AccountNumber { get; set; }
        public string CustomerName { get; set; }
        public CustomerContactViewModel CustomerContact { get; set; }
        public IEnumerable<SelectListItem> ContactTypes { get; set; }
    }
}