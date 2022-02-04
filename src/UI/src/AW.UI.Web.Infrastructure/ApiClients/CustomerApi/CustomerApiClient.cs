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
using AW.UI.Web.Infrastructure.ApiClients.CustomerApi.Exceptions;

namespace AW.UI.Web.Infrastructure.ApiClients.CustomerApi
{
    public class CustomerApiClient : ICustomerApiClient
    {
        private readonly HttpClient client;
        private readonly ILogger<CustomerApiClient> logger;

        public CustomerApiClient(HttpClient client, ILogger<CustomerApiClient> logger)
            => (this.client, this.logger) = (client, logger);

        public async Task<Models.GetCustomers.GetCustomersResponse> GetCustomersAsync(
            int pageIndex, 
            int pageSize, 
            string territory, 
            sh_int.CustomerType? customerType,
            string accountNumber
        )
        {
            string requestUri = $"/customer-api/Customer?api-version=1.0&pageIndex={pageIndex}&pageSize={pageSize}";
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

            return await stream.DeserializeAsync<Models.GetCustomers.GetCustomersResponse>(
                new JsonSerializerOptions
                {
                    Converters =
                    {
                        new JsonStringEnumConverter(),
                        new CustomerConverter<
                            Models.GetCustomers.Customer,
                            Models.GetCustomers.StoreCustomer,
                            Models.GetCustomers.IndividualCustomer>()
                    },
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                }
            );
        }

        public async Task<Models.GetCustomer.Customer> GetCustomerAsync(string accountNumber)
        {
            return await GetCustomerAsync<Models.GetCustomer.Customer>(accountNumber);
        }

        public async Task<T> GetCustomerAsync<T>(string accountNumber)
        {
            logger.LogInformation("Getting customer with account number {AccountNumber}", accountNumber);

            try
            {
                using var response = await client.GetAsync($"customer-api/Customer/{accountNumber}?&api-version=1.0");
                response.EnsureSuccessStatusCode();
                var stream = await response.Content.ReadAsStreamAsync();

                return await stream.DeserializeAsync<T>(
                    new JsonSerializerOptions
                    {
                        Converters =
                        {
                        new JsonStringEnumConverter(),
                        new CustomerConverter<
                            Models.GetCustomer.Customer,
                            Models.GetCustomer.StoreCustomer,
                            Models.GetCustomer.IndividualCustomer>()
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

        public async Task<Models.UpdateCustomer.Customer> UpdateCustomerAsync(string accountNumber, Models.UpdateCustomer.Customer customer)
        {
            logger.LogInformation("Updating customer with account number {AccountNumber}", accountNumber);
            string requestUri = $"customer-api/Customer/{accountNumber}?&api-version=1.0";

            string json = JsonSerializer.Serialize(customer, new JsonSerializerOptions
            {
                Converters =
                    {
                        new JsonStringEnumConverter(),
                        new CustomerConverter<
                            Models.UpdateCustomer.Customer,
                            Models.UpdateCustomer.StoreCustomer,
                            Models.UpdateCustomer.IndividualCustomer>()
                    },
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            logger.LogInformation("Calling PUT method on {RequestUri} with {JSON}", requestUri, json);

            using var response = await client.PutAsync(
                requestUri,
                new StringContent(json, Encoding.UTF8, "application/json")
            );
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();
            var updatedCustomer = await stream.DeserializeAsync<Models.UpdateCustomer.Customer>(new JsonSerializerOptions
            {
                Converters =
                    {
                        new JsonStringEnumConverter(),
                        new CustomerConverter<
                            Models.UpdateCustomer.Customer,
                            Models.UpdateCustomer.StoreCustomer,
                            Models.UpdateCustomer.IndividualCustomer>()
                    },
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            logger.LogInformation("Returning customer {@Customer}", updatedCustomer);
            return updatedCustomer;
        }
    }
}