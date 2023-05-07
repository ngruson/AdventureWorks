using AW.UI.Web.Store.Mvc.ViewModels;
using AW.UI.Web.Store.Mvc.ViewModels.Cart;

namespace AW.UI.Web.Store.Mvc.Services
{
    public interface IBasketService
    {
        Task<T> GetBasketAsync<T>(string? userID);
        Task<Infrastructure.Api.Basket.Handlers.GetBasket.Basket> AddBasketItemAsync(ApplicationUser user, string? productNumber, int quantity);
        Task<Infrastructure.Api.Basket.Handlers.GetBasket.Basket> SetQuantities(ApplicationUser user, Dictionary<string, int> quantities);
        Task<CheckoutViewModel> Checkout(ApplicationUser user);
        Task Checkout(Infrastructure.Api.Basket.Handlers.Checkout.BasketCheckout basket, string customerNumber);
    }
}