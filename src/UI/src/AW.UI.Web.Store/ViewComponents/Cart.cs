using AW.UI.Web.Store.Services;
using AW.UI.Web.Store.ViewModels;
using AW.UI.Web.Store.ViewModels.Cart;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AW.UI.Web.Store.ViewComponents
{
    public class Cart : ViewComponent
    {
        private readonly IBasketService basketService;

        public Cart(IBasketService basketService) => this.basketService = basketService;

        public async Task<IViewComponentResult> InvokeAsync(ApplicationUser user)
        {
            var vm = new CartComponentViewModel();
            try
            {
                var itemsInCart = await ItemsInCartAsync(user);
                vm.ItemsCount = itemsInCart;
                return View(vm);
            }
            catch
            {
                ViewBag.IsBasketInoperative = true;
            }

            return View(vm);
        }
        private async Task<int> ItemsInCartAsync(ApplicationUser user)
        {
            var basket = await basketService.GetBasketAsync<Basket>(user.Id);
            return basket.Items.Count;
        }
    }
}