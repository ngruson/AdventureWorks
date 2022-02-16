﻿using AW.UI.Web.Infrastructure.ApiClients.ReferenceDataApi.Models.GetStateProvinces;
using AW.UI.Web.Store.Services;
using AW.UI.Web.Store.ViewModels;
using AW.UI.Web.Store.ViewModels.Cart;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AW.UI.Web.Store.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly IBasketService basketService;
        private readonly IIdentityParser<ApplicationUser> appUserParser;
        private readonly IReferenceDataService referenceDataService;
        private readonly ICustomerService customerService;

        public CartController(
            IBasketService basketService, 
            IIdentityParser<ApplicationUser> appUserParser,
            IReferenceDataService referenceDataService,
            ICustomerService customerService
        ) =>
            (this.basketService, this.appUserParser, this.referenceDataService, this.customerService) = 
                (basketService, appUserParser, referenceDataService, customerService);

        public async Task<IActionResult> Index()
        {
            try
            {
                var vm = await GetBasketAsync<Basket>();
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
                var user = appUserParser.Parse(HttpContext.User);
                var vm = await basketService.Checkout(user);

                return View(vm);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }

            return View();
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

        public async Task<IEnumerable<StateProvince>> GetStatesProvinces(string country)
        {
            var stateProvinces = await referenceDataService.GetStatesProvincesAsync(country);
            return stateProvinces;
        }

        private async Task<T> GetBasketAsync<T>()
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