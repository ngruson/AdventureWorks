using AW.UI.Web.Store.ViewModels;
using System.Threading.Tasks;

namespace AW.UI.Web.Store.Services
{
    public interface IBasketService
    {
        Task<Basket> GetBasketAsync(string userID);
        Task<Basket> AddBasketItemAsync(ApplicationUser user, string productNumber, int quantity);
    }
}