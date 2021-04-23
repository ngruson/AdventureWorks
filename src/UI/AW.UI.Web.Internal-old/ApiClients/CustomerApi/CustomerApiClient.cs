using AW.UI.Web.Internal.ApiClients.CustomerApi.Models.GetCustomers;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace AW.UI.Web.Internal.ApiClients.CustomerApi
{
    public class CustomerApiClient : ICustomerApiClient
    {
        private readonly HttpClient client;
        private readonly ILogger<CustomerApiClient> logger;

        public CustomerApiClient(HttpClient client, ILogger<CustomerApiClient> logger) =>
            (this.client, this.logger) = (client, logger);

        public async Task<GetCustomersResponse> GetCustomersAsync(int pageIndex, int pageSize, string territory, CustomerType? customerType)
        {
            string requestUri = $"Customer?&api-version=1.0&pageIndex={pageIndex}&pageSize={pageSize}";
            string logMessage = "Getting customers with page index {PageIndex}, page size {PageSize}";
            
            List<object> args = new List<object> { pageIndex, pageSize };


            if (!string.IsNullOrEmpty(territory))
            {
                logMessage += ", territory {Territory}";
                args.Add(territory);
                requestUri += $"&territory={territory}";
            };
            if (customerType.HasValue)
            {
                var customerTypeValue = customerType.Value == CustomerType.Individual ? 0 : 1;
                logMessage += ", customer type {CustomerType}";
                args.Add(customerType);
                requestUri += $"&customerType={customerTypeValue}";
            }

            logger.LogInformation(logMessage);

            using var response = await client.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();

            return await stream.DeserializeAsync<GetCustomersResponse>(
                new JsonSerializerOptions
                {
                    //Converters =
                    //{
                    //    new CustomerConverter(),
                    //    new StoreCustomerConverter(),
                    //    new IndividualCustomerConverter()
                    //}
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

            using var response = await client.GetAsync($"Customer/{accountNumber}?&api-version=1.0");
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();

            return await stream.DeserializeAsync<T>(
                new JsonSerializerOptions
                {
                    //Converters =
                    //{
                    //    new CustomerConverter(),
                    //    new StoreCustomerConverter(),
                    //    new IndividualCustomerConverter()
                    //}
                }
            );
        }

        public async Task<Models.UpdateCustomer.Customer> UpdateCustomerAsync(string accountNumber, Models.UpdateCustomer.Customer customer)
        {
            logger.LogInformation("Updating customer with account number {AccountNumber}", accountNumber);
            string requestUri = $"Customer/{accountNumber}?&api-version=1.0";

            string json = JsonSerializer.Serialize(customer);
            logger.LogInformation("Calling PUT method on {RequestUri} with {JSON}", requestUri, json);

            using var response = await client.PutAsync(
                requestUri,
                new StringContent(json)
            );
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();
            var updatedCustomer = await stream.DeserializeAsync<Models.UpdateCustomer.Customer>();

            logger.LogInformation("Returning customer {@Customer}", updatedCustomer);
            return updatedCustomer;
        }
    }
}