using AW.SharedKernel.Interfaces;
using GetCustomers = AW.UI.Web.Infrastructure.Api.Customer.Handlers.GetCustomers;

namespace AW.UI.Web.Infrastructure.Api.Interfaces
{
    public interface ICustomerApiClient
    {
        Task<List<GetCustomers.Customer?>?> GetCustomersAsync(CustomerType? customerType = null);
        Task<Customer.Handlers.GetCustomer.Customer?> GetCustomerAsync(Guid objectId);
        Task<T?> GetCustomerAsync<T>(Guid objectId);
        Task<Customer.Handlers.GetPreferredAddress.Address?> GetPreferredAddressAsync(string? accountNumber, string? addressType);
        Task<Customer.Handlers.UpdateCustomer.Customer?> UpdateCustomerAsync(Customer.Handlers.UpdateCustomer.Customer customer);
    }
}
