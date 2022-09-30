using sh_int = AW.SharedKernel.Interfaces;
using AW.SharedKernel.JsonConverters;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System;
using GetCustomers = AW.UI.Web.SharedKernel.Customer.Handlers.GetCustomers;
using GetCustomer = AW.UI.Web.SharedKernel.Customer.Handlers.GetCustomer;
using UpdateCustomer = AW.UI.Web.SharedKernel.Customer.Handlers.UpdateCustomer;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using Microsoft.Extensions.DependencyInjection;

namespace AW.UI.Web.Infrastructure.ApiClients
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
            UpdateCustomer.Customer,
            UpdateCustomer.StoreCustomer,
            UpdateCustomer.IndividualCustomer> _converterUpdateCustomer;

        public CustomerApiClient(
            HttpClient client, 
            ILogger<CustomerApiClient> logger,
            CustomerConverter<GetCustomers.Customer, GetCustomers.StoreCustomer, GetCustomers.IndividualCustomer> converterGetCustomers,
            CustomerConverter<GetCustomer.Customer, GetCustomer.StoreCustomer, GetCustomer.IndividualCustomer> converterGetCustomer,
            CustomerConverter<UpdateCustomer.Customer, UpdateCustomer.StoreCustomer, UpdateCustomer.IndividualCustomer> converterUpdateCustomer
        ) => (_client, _logger, _converterGetCustomers, _converterGetCustomer, _converterUpdateCustomer) = (client, logger, converterGetCustomers, converterGetCustomer, converterUpdateCustomer);

        public async Task<GetCustomers.GetCustomersResponse> GetCustomersAsync(
            int pageIndex,
            int pageSize,
            string territory,
            sh_int.CustomerType? customerType,
            string accountNumber
        )
        {
            string requestUri = $"Customer?api-version=1.0&pageIndex={pageIndex}&pageSize={pageSize}";
            string logMessage = "Getting customers with page index {PageIndex}, page size {PageSize}";

            var args = new List<object> { pageIndex, pageSize };

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

            return await stream.DeserializeAsync<GetCustomers.GetCustomersResponse>(
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

        public async Task<GetCustomer.Customer> GetCustomerAsync(string accountNumber)
        {
            return await GetCustomerAsync<GetCustomer.Customer>(accountNumber);
        }

        public async Task<T> GetCustomerAsync<T>(string accountNumber)
        {
            _logger.LogInformation("Getting customer with account number {AccountNumber}", accountNumber);

            try
            {
                using var response = await _client.GetAsync($"Customer/{accountNumber}?&api-version=1.0");
                response.EnsureSuccessStatusCode();
                var stream = await response.Content.ReadAsStreamAsync();

                return await stream.DeserializeAsync<T>(
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

        public async Task<SharedKernel.Customer.Handlers.GetPreferredAddress.Address> GetPreferredAddressAsync(string accountNumber, string addressType)
        {
            _logger.LogInformation("Getting preferred address for address type {AddressType} for customer {AccountNumber}", addressType, accountNumber);

            try
            {
                using var response = await _client.GetAsync($"Customer/{accountNumber}/preferredAddress/{addressType}?&api-version=1.0");
                response.EnsureSuccessStatusCode();
                var stream = await response.Content.ReadAsStreamAsync();

                return await stream.DeserializeAsync<SharedKernel.Customer.Handlers.GetPreferredAddress.Address>(
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

        public async Task<UpdateCustomer.Customer> UpdateCustomerAsync(string accountNumber, UpdateCustomer.Customer customer)
        {
            _logger.LogInformation("Updating customer with account number {AccountNumber}", accountNumber);
            string requestUri = $"Customer/{accountNumber}?&api-version=1.0";

            var options = new JsonSerializerOptions
            {
                Converters =
                    {
                        new JsonStringEnumConverter(),
                        _converterUpdateCustomer
                    },
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            string json = JsonSerializer.Serialize(customer, options);
            _logger.LogInformation("Calling PUT method on {RequestUri} with {JSON}", requestUri, json);

            using var response = await _client.PutAsync(
                requestUri,
                new StringContent(json, Encoding.UTF8, "application/json")
            );
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();
            var updatedCustomer = await stream.DeserializeAsync<SharedKernel.Customer.Handlers.UpdateCustomer.Customer>(
                options
            );

            _logger.LogInformation("Returning customer {@Customer}", updatedCustomer);
            return updatedCustomer;
        }
    }
}