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
using AW.UI.Web.SharedKernel.Customer.Handlers.GetCustomers;
using AW.UI.Web.SharedKernel.Interfaces.Api;

namespace AW.UI.Web.Infrastructure.ApiClients
{
    public class CustomerApiClient : ICustomerApiClient
    {
        private readonly HttpClient client;
        private readonly ILogger<CustomerApiClient> logger;

        public CustomerApiClient(HttpClient client, ILogger<CustomerApiClient> logger)
            => (this.client, this.logger) = (client, logger);

        public async Task<GetCustomersResponse> GetCustomersAsync(
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

            logger.LogInformation(logMessage, args.ToArray());

            using var response = await client.GetAsync(requestUri);

            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();

            return await stream.DeserializeAsync<GetCustomersResponse>(
                new JsonSerializerOptions
                {
                    Converters =
                    {
                        new JsonStringEnumConverter(),
                        new CustomerConverter<
                            Customer,
                            StoreCustomer,
                            IndividualCustomer>()
                    },
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                }
            );
        }

        public async Task<SharedKernel.Customer.Handlers.GetCustomer.Customer> GetCustomerAsync(string accountNumber)
        {
            return await GetCustomerAsync<SharedKernel.Customer.Handlers.GetCustomer.Customer>(accountNumber);
        }

        public async Task<T> GetCustomerAsync<T>(string accountNumber)
        {
            logger.LogInformation("Getting customer with account number {AccountNumber}", accountNumber);

            try
            {
                using var response = await client.GetAsync($"Customer/{accountNumber}?&api-version=1.0");
                response.EnsureSuccessStatusCode();
                var stream = await response.Content.ReadAsStreamAsync();

                return await stream.DeserializeAsync<T>(
                    new JsonSerializerOptions
                    {
                        Converters =
                        {
                        new JsonStringEnumConverter(),
                        new CustomerConverter<
                            SharedKernel.Customer.Handlers.GetCustomer.Customer,
                            SharedKernel.Customer.Handlers.GetCustomer.StoreCustomer,
                            SharedKernel.Customer.Handlers.GetCustomer.IndividualCustomer>()
                        },
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    }
                );
            }
            catch (Exception ex)
            {
                logger.LogError("Getting customer {AccountNumber} failed", accountNumber);
                throw new CustomerApiClientException($"Getting customer {accountNumber} failed", ex);
            }
        }

        public async Task<SharedKernel.Customer.Handlers.GetPreferredAddress.Address> GetPreferredAddressAsync(string accountNumber, string addressType)
        {
            logger.LogInformation("Getting preferred address for address type {AddressType} for customer {AccountNumber}", addressType, accountNumber);

            try
            {
                using var response = await client.GetAsync($"Customer/{accountNumber}/preferredAddress/{addressType}?&api-version=1.0");
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
                logger.LogError("Getting preferred address for address type {AddressType} for customer {AccountNumber} failed", addressType, accountNumber);
                throw new CustomerApiClientException($"Getting preferred address for address type {addressType} for customer {accountNumber} failed", ex);
            }
        }

        public async Task<SharedKernel.Customer.Handlers.UpdateCustomer.Customer> UpdateCustomerAsync(string accountNumber, SharedKernel.Customer.Handlers.UpdateCustomer.Customer customer)
        {
            logger.LogInformation("Updating customer with account number {AccountNumber}", accountNumber);
            string requestUri = $"Customer/{accountNumber}?&api-version=1.0";

            var options = new JsonSerializerOptions
            {
                Converters =
                    {
                        new JsonStringEnumConverter(),
                        new CustomerConverter<
                            SharedKernel.Customer.Handlers.UpdateCustomer.Customer,
                            SharedKernel.Customer.Handlers.UpdateCustomer.StoreCustomer,
                            SharedKernel.Customer.Handlers.UpdateCustomer.IndividualCustomer>()
                    },
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            string json = JsonSerializer.Serialize(customer, options);
            logger.LogInformation("Calling PUT method on {RequestUri} with {JSON}", requestUri, json);

            using var response = await client.PutAsync(
                requestUri,
                new StringContent(json, Encoding.UTF8, "application/json")
            );
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();
            var updatedCustomer = await stream.DeserializeAsync<SharedKernel.Customer.Handlers.UpdateCustomer.Customer>(
                options
            );

            logger.LogInformation("Returning customer {@Customer}", updatedCustomer);
            return updatedCustomer;
        }
    }
}