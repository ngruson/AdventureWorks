using AW.Services.Basket.Core.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AW.Services.Basket.Core
{
    public interface IBasketRepository
    {
        Task<CustomerBasket> GetBasketAsync(string customerId);
        IEnumerable<string> GetUsers();
        Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket);
        Task<bool> DeleteBasketAsync(string id);
    }
}