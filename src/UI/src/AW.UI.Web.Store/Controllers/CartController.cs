using AW.UI.Web.Store.Services;
using AW.UI.Web.Store.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AW.UI.Web.Store.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly IBasketService basketService;
        private readonly IIdentityParser<ApplicationUser> appUserParser;

        public CartController(IBasketService basketService, IIdentityParser<ApplicationUser> appUserParser) =>
            (this.basketService, this.appUserParser) = (basketService, appUserParser);

        public async Task<IActionResult> Index()
        {
            try
            {
                var user = appUserParser.Parse(HttpContext.User);
                var vm = await basketService.GetBasketAsync(user.Id);

                return View(vm);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }

            return View();
        }

        public async Task<IActionResult> AddToCart(string productNumber)
        {
            try
            {
                if (!string.IsNullOrEmpty(productNumber))
                {
                    var user = appUserParser.Parse(HttpContext.User);
                    await basketService.AddBasketItemAsync(user, productNumber, 1);
                }
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                // Catch error when Basket.api is in circuit-opened mode                 
                HandleException(ex);
            }

            return RedirectToAction("Index", "Home", new { errorMsg = ViewBag.BasketInoperativeMsg });
        }

        private void HandleException(Exception ex)
        {
            ViewBag.BasketInoperativeMsg = $"Basket Service is inoperative {ex.GetType().Name} - {ex.Message}";
        }
    }
}