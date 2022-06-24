using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace AW.UI.Web.Store.ViewModels.Cart
{
    public class CheckoutViewModel
    {
        public BasketCheckout Basket { get; set; }
        public List<SelectListItem> Countries { get; set; }
        public List<SelectListItem> StatesProvinces_Billing { get; set; }
        public List<SelectListItem> StatesProvinces_Shipping { get; set; }
        public List<SelectListItem> CardTypes { get; set; }
        public List<SelectListItem> ShipMethods { get; set; }
    }
}