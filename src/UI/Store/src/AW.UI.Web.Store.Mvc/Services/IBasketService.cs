﻿using AW.UI.Web.Store.Mvc.ViewModels;
using AW.UI.Web.Store.Mvc.ViewModels.Cart;

namespace AW.UI.Web.Store.Mvc.Services
{
    public interface IBasketService
    {
        Task<T> GetBasketAsync<T>(string? userID);
        Task<SharedKernel.Basket.Handlers.GetBasket.Basket> AddBasketItemAsync(ApplicationUser user, string? productNumber, int quantity);
        Task<SharedKernel.Basket.Handlers.GetBasket.Basket> SetQuantities(ApplicationUser user, Dictionary<string, int> quantities);
        Task<CheckoutViewModel> Checkout(ApplicationUser user);
        Task Checkout(SharedKernel.Basket.Handlers.Checkout.BasketCheckout basket, string customerNumber);
    }
}