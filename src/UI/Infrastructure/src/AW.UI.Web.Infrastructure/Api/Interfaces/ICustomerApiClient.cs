﻿using AW.SharedKernel.Interfaces;
using AW.UI.Web.Infrastructure.Api.Customer.Handlers.GetCustomers;

namespace AW.UI.Web.Infrastructure.Api.Interfaces
{
    public interface ICustomerApiClient
    {
        Task<GetCustomersResponse?> GetCustomersAsync(int pageIndex, int pageSize, string? territory, CustomerType? customerType, string? accountNumber);
        Task<Customer.Handlers.GetCustomer.Customer?> GetCustomerAsync(string? accountNumber);
        Task<T?> GetCustomerAsync<T>(string? accountNumber);
        Task<Customer.Handlers.GetPreferredAddress.Address?> GetPreferredAddressAsync(string? accountNumber, string? addressType);
        Task<Customer.Handlers.UpdateCustomer.Customer?> UpdateCustomerAsync(string? accountNumber, Customer.Handlers.UpdateCustomer.Customer customer);
    }
}