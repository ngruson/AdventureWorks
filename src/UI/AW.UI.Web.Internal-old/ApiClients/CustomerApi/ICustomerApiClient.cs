using AW.UI.Web.Internal.ApiClients.CustomerApi.Models.GetCustomers;
using System.Threading.Tasks;

namespace AW.UI.Web.Internal.ApiClients.CustomerApi
{
    public interface ICustomerApiClient
    {
        Task<GetCustomersResponse> GetCustomersAsync(int pageIndex, int pageSize, string territory, CustomerType? customerType);
        Task<Models.GetCustomer.Customer> GetCustomerAsync(string accountNumber);
        Task<T> GetCustomerAsync<T>(string accountNumber);
        Task<Models.UpdateCustomer.Customer> UpdateCustomerAsync(string accountNumber, Models.UpdateCustomer.Customer customer);
    }
}