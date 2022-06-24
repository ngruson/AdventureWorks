using models = AW.UI.Web.Infrastructure.ApiClients.CustomerApi.Models;
using System.Threading.Tasks;

namespace AW.UI.Web.Store.Services
{
    public interface ICustomerService
    {
        Task<models.GetCustomer.Customer> GetCustomerAsync(string customerNumber);
        Task<models.GetPreferredAddress.Address> GetPreferredAddressAsync(string customerNumber, string addressType);
    }
}