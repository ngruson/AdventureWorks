using AW.Services.Basket.Core.Models;

namespace AW.Services.Basket.Core
{
    public interface IBasketRepository
    {
        Task<CustomerBasket?> GetBasketAsync(string customerId);
        IEnumerable<string> GetUsers();
        Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket);
        Task<bool> DeleteBasketAsync(string id);
    }
}