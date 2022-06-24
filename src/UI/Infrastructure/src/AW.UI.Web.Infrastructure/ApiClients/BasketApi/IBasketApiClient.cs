using AW.UI.Web.Infrastructure.ApiClients.BasketApi.Models;
using System.Threading.Tasks;

namespace AW.UI.Web.Infrastructure.ApiClients.BasketApi
{
    public interface IBasketApiClient
    {
        Task<Basket> GetBasket(string userID);
        Task<Basket> UpdateBasket(Basket basket);
        Task Checkout(BasketCheckout basket);
    }
}