using AW.UI.Web.Infrastructure.ApiClients.CustomerApi.Models.GetCustomer;
using System.Threading.Tasks;

namespace AW.UI.Web.Store.Services
{
    public interface ICustomerService
    {
        Task<Customer> GetCustomerAsync(string customerNumber);
    }
}