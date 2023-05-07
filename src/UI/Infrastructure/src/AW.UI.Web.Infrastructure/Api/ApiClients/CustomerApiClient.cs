using sh_int = AW.SharedKernel.Interfaces;
using AW.SharedKernel.JsonConverters;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using AW.UI.Web.Infrastructure.Api.Interfaces;
using GetCustomer = AW.UI.Web.Infrastructure.Api.Customer.Handlers.GetCustomer;
using GetCustomers = AW.UI.Web.Infrastructure.Api.Customer.Handlers.GetCustomers;
using UpdateCustomer = AW.UI.Web.Infrastructure.Api.Customer.Handlers.UpdateCustomer;

namespace AW.UI.Web.Infrastructure.Api.ApiClients
{
    public class CustomerApiClient : ICustomerApiClient
    {
        private readonly HttpClient _client;
        private readonly ILogger<CustomerApiClient> _logger;

        private readonly CustomerConverter<
            GetCustomers.Customer,
            GetCustomers.StoreCustomer,
            GetCustomers.IndividualCustomer> _converterGetCustomers;

        private readonly CustomerConverter<
            GetCustomer.Customer,
            GetCustomer.StoreCustomer,
            GetCustomer.IndividualCustomer> _converterGetCustomer;

        private readonly CustomerConverter<
            Customer.Handlers.UpdateCustomer.Customer,
            Customer.Handlers.UpdateCustomer.StoreCustomer,
            Customer.Handlers.UpdateCustomer.IndividualCustomer> _converterUpdateCustomer;

        public CustomerApiClient(
            HttpClient client,
            ILogger<CustomerApiClient> logger,
            CustomerConverter<GetCustomers.Customer, GetCustomers.StoreCustomer, GetCustomers.IndividualCustomer> converterGetCustomers,
            CustomerConverter<GetCustomer.Customer, GetCustomer.StoreCustomer, GetCustomer.IndividualCustomer> converterGetCustomer,
            CustomerConverter<UpdateCustomer.Customer, UpdateCustomer.StoreCustomer, Customer.Handlers.UpdateCustomer.IndividualCustomer> converterUpdateCustomer
        ) => (_client, _logger, _converterGetCustomers, _converterGetCustomer, _converterUpdateCustomer) = (client, logger, converterGetCustomers, converterGetCustomer, converterUpdateCustomer);

        public async Task<GetCustomers.GetCustomersResponse?> GetCustomersAsync(
            int pageIndex,
            int pageSize,
            string? territory,
            sh_int.CustomerType? customerType,
            string? accountNumber
        )
        {
            var requestUri = $"Customer?api-version=1.0&pageIndex={pageIndex}&pageSize={pageSize}";
            var logMessage = "Getting customers with page index {PageIndex}, page size {PageSize}";

            var args = new List<object?> { pageIndex, pageSize };

            if (!string.IsNullOrEmpty(territory))
            {
                logMessage += ", territory {Territory}";
                args.Add(territory);
                requestUri += $"&territory={territory}";
            }
            if (customerType.HasValue)
            {
                var customerTypeValue = customerType.Value == sh_int.CustomerType.Individual ? 0 : 1;
                logMessage += ", customer type {CustomerType}";
                args.Add(customerType);
                requestUri += $"&customerType={customerTypeValue}";
            }
            if (!string.IsNullOrEmpty(accountNumber))
            {
                logMessage += ", account number {AccountNumber}";
                args.Add(accountNumber);
                requestUri += $"&accountNumber={accountNumber}";
            }

            _logger.LogInformation(logMessage, args.ToArray());

            using var response = await _client.GetAsync(requestUri);

            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();

            return await stream.DeserializeAsync<GetCustomers.GetCustomersResponse?>(
                new JsonSerializerOptions
                {
                    Converters =
                    {
                        new JsonStringEnumConverter(),
                        _converterGetCustomers
                    },
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                }
            );
        }

        public async Task<Customer.Handlers.GetCustomer.Customer?> GetCustomerAsync(string? accountNumber)
        {
            return await GetCustomerAsync<Customer.Handlers.GetCustomer.Customer?>(accountNumber);
        }

        public async Task<T?> GetCustomerAsync<T>(string? accountNumber)
        {
            _logger.LogInformation("Getting customer with account number {AccountNumber}", accountNumber);

            try
            {
                using var response = await _client.GetAsync($"Customer/{accountNumber}?&api-version=1.0");
                response.EnsureSuccessStatusCode();
                var stream = await response.Content.ReadAsStreamAsync();

                return await stream.DeserializeAsync<T?>(
                    new JsonSerializerOptions
                    {
                        Converters =
                        {
                            new JsonStringEnumConverter(),
                            _converterGetCustomer
                        },
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    }
                );
            }
            catch (Exception ex)
            {
                _logger.LogError("Getting customer {AccountNumber} failed", accountNumber);
                throw new CustomerApiClientException($"Getting customer {accountNumber} failed", ex);
            }
        }

        public async Task<Customer.Handlers.GetPreferredAddress.Address?> GetPreferredAddressAsync(string? accountNumber, string? addressType)
        {
            _logger.LogInformation("Getting preferred address for address type {AddressType} for customer {AccountNumber}", addressType, accountNumber);

            try
            {
                using var response = await _client.GetAsync($"Customer/{accountNumber}/preferredAddress/{addressType}?&api-version=1.0");
                response.EnsureSuccessStatusCode();
                var stream = await response.Content.ReadAsStreamAsync();

                return await stream.DeserializeAsync<Customer.Handlers.GetPreferredAddress.Address?>(
                    new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    }
                );
            }
            catch (Exception ex)
            {
                _logger.LogError("Getting preferred address for address type {AddressType} for customer {AccountNumber} failed", addressType, accountNumber);
                throw new CustomerApiClientException($"Getting preferred address for address type {addressType} for customer {accountNumber} failed", ex);
            }
        }

        public async Task<Customer.Handlers.UpdateCustomer.Customer?> UpdateCustomerAsync(string? accountNumber, Customer.Handlers.UpdateCustomer.Customer customer)
        {
            _logger.LogInformation("Updating customer with account number {AccountNumber}", accountNumber);
            var requestUri = $"Customer/{accountNumber}?&api-version=1.0";

            var options = new JsonSerializerOptions
            {
                Converters =
                    {
                        new JsonStringEnumConverter(),
                        _converterUpdateCustomer
                    },
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var json = JsonSerializer.Serialize(customer, options);
            _logger.LogInformation("Calling PUT method on {RequestUri} with {JSON}", requestUri, json);

            using var response = await _client.PutAsync(
                requestUri,
                new StringContent(json, Encoding.UTF8, "application/json")
            );
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();
            var updatedCustomer = await stream.DeserializeAsync<Customer.Handlers.UpdateCustomer.Customer>(
                options
            );

            _logger.LogInformation("Returning customer {@Customer}", updatedCustomer);
            return updatedCustomer;
        }
    }
}
