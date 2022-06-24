using AW.UI.Web.Store.ViewModels;
using AW.UI.Web.Store.ViewModels.Cart;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AW.UI.Web.Store.Services
{
    public interface IBasketService
    {
        Task<T> GetBasketAsync<T>(string userID);
        Task<Basket> AddBasketItemAsync(ApplicationUser user, string productNumber, int quantity);
        Task<Basket> SetQuantities(ApplicationUser user, Dictionary<string, int> quantities);
        Task<CheckoutViewModel> Checkout(ApplicationUser user);
        Task Checkout(Basket basket, string customerNumber);
    }
}