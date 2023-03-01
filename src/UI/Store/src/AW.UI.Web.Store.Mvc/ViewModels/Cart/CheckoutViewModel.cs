using Microsoft.AspNetCore.Mvc.Rendering;

namespace AW.UI.Web.Store.Mvc.ViewModels.Cart
{
    public class CheckoutViewModel
    {
        public BasketCheckout? Basket { get; set; }
        public List<SelectListItem>? Countries { get; set; }
        public List<SelectListItem>? StatesProvinces_Billing { get; set; }
        public List<SelectListItem>? StatesProvinces_Shipping { get; set; }
        public List<SelectListItem>? CardTypes { get; set; }
        public List<SelectListItem>? ShipMethods { get; set; }
    }
}