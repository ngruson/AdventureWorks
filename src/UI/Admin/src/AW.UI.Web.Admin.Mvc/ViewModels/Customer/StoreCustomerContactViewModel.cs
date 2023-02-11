using Microsoft.AspNetCore.Mvc.Rendering;

namespace AW.UI.Web.Admin.Mvc.ViewModels.Customer
{
    public class StoreCustomerContactViewModel
    {
        public bool IsNewContact { get; set; }
        public string? AccountNumber { get; set; }
        public string? CustomerName { get; set; }
        public CustomerContactViewModel? CustomerContact { get; set; }
        public IEnumerable<SelectListItem>? ContactTypes { get; set; }
        public IEnumerable<SelectListItem?>? PhoneNumberTypes { get; set; }
    }
}