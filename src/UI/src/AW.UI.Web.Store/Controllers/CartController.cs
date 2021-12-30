using AW.UI.Web.Store.Services;
using AW.UI.Web.Store.ViewModels;
using AW.UI.Web.Store.ViewModels.Cart;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AW.UI.Web.Store.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly IBasketService basketService;
        private readonly IIdentityParser<ApplicationUser> appUserParser;
        private readonly IReferenceDataService referenceDataService;

        public CartController(
            IBasketService basketService, 
            IIdentityParser<ApplicationUser> appUserParser,
            IReferenceDataService referenceDataService) =>
            (this.basketService, this.appUserParser, this.referenceDataService) = 
                (basketService, appUserParser, referenceDataService);

        public async Task<IActionResult> Index()
        {
            try
            {
                var vm = await GetBasket<Basket>();
                return View(vm);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(Dictionary<string, int> quantities, string action)
        {
            try
            {
                var user = appUserParser.Parse(HttpContext.User);
                await basketService.SetQuantities(user, quantities);
                if (action == "Checkout")
                {
                    return RedirectToAction("Checkout", "Cart");
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }

            return RedirectToAction("Index");
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

        public async Task<IActionResult> Checkout()
        {
            try
            {
                var vm = new CheckoutViewModel
                {
                    Basket = await GetBasket<BasketCheckout>(),
                    Countries = await GetCountries(),
                    CardTypes = GetCardTypes(),
                    ShipMethods = await GetShipMethods()
                };

                return View(vm);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }

            return View();
        }

        private async Task<List<SelectListItem>> GetCountries()
        {
            var countries = await referenceDataService.GetCountriesAsync();

            var items = countries
                .Select(t => new SelectListItem() { Value = t.CountryRegionCode, Text = t.Name })
                .OrderBy(b => b.Text)
                .ToList();

            var allItem = new SelectListItem() { Value = "", Text = "--Select country--", Selected = true };
            items.Insert(0, allItem);

            return items;
        }

        private List<SelectListItem> GetCardTypes()
        {
            return new List<SelectListItem>
            {
                new SelectListItem("--Select card type--", null),
                new SelectListItem("SuperiorCard", "SuperiorCard"),
                new SelectListItem("Vista", "Vista"),
                new SelectListItem("Distinguish", "Distinguish"),
                new SelectListItem("ColonialVoice", "ColonialVoice")
            };
        }

        private async Task<List<SelectListItem>> GetShipMethods()
        {
            var shipMethods = await referenceDataService.GetShipMethodsAsync();

            var items = shipMethods
                .Select(s => new SelectListItem() { Value = s.Name, Text = s.Name })
                .OrderBy(b => b.Text)
                .ToList();

            var allItem = new SelectListItem() { Value = "", Text = "--Select shipping method--", Selected = true };
            items.Insert(0, allItem);

            return items;
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = appUserParser.Parse(HttpContext.User);
                    await basketService.Checkout(model.Basket, user.CustomerNumber);

                    //Redirect to historic list.
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", $"It was not possible to create a new order, please try later on ({ex.GetType().Name} - {ex.Message})");
            }

            return View(model);
        }

        private async Task<T> GetBasket<T>()
        {
            var user = appUserParser.Parse(HttpContext.User);
            return await basketService.GetBasketAsync<T>(user.Id);
        }

        private void HandleException(Exception ex)
        {
            ViewBag.BasketInoperativeMsg = $"Basket Service is inoperative {ex.GetType().Name} - {ex.Message}";
        }
    }
}