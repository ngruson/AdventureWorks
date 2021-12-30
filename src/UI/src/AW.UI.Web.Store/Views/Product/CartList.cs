using AW.UI.Web.Store.Services;
using AW.UI.Web.Store.ViewModels;
using AW.UI.Web.Store.ViewModels.Cart;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AW.UI.Web.Store.ViewComponents
{
    public class CartList : ViewComponent
    {
        private readonly IBasketService basketService;

        public CartList(IBasketService basketService) => this.basketService = basketService;

        public async Task<IViewComponentResult> InvokeAsync(ApplicationUser user)
        {
            var vm = new Basket();
            try
            {
                vm = await GetItemsAsync(user);
                return View(vm);
            }
            catch (Exception ex)
            {
                ViewBag.BasketInoperativeMsg = $"Basket Service is inoperative, please try later on. ({ex.GetType().Name} - {ex.Message}))";
            }

            return View(vm);
        }

        private Task<Basket> GetItemsAsync(ApplicationUser user) => basketService.GetBasketAsync<Basket>(user.Id);
    }
}