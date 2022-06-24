using AW.SharedKernel.JsonConverters;
using AW.UI.Web.Infrastructure.ApiClients.SalesOrderApi.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AW.UI.Web.Infrastructure.ApiClients.SalesOrderApi
{
    public class SalesOrderApiClient : ISalesOrderApiClient
    {
        private readonly HttpClient client;
        private readonly ILogger<SalesOrderApiClient> logger;

        public SalesOrderApiClient(HttpClient client, ILogger<SalesOrderApiClient> logger) =>
            (this.client, this.logger) = (client, logger);

        public async Task<SalesOrdersResult> GetSalesOrdersAsync(int pageIndex, int pageSize, string territory, CustomerType? customerType)
        {
            string requestUri = $"/salesorder-api/SalesOrder?&api-version=1.0&pageIndex={pageIndex}&pageSize={pageSize}";
            string logMessage = "Getting sales orders with page index {PageIndex}, page size {PageSize}";

            List<object> args = new List<object> { pageIndex, pageSize };

            if (!string.IsNullOrEmpty(territory))
            {
                logMessage += ", territory {Territory}";
                args.Add(territory);
                requestUri += $"&territory={territory}";
            }
            if (customerType.HasValue)
            {
                var customerTypeValue = customerType.Value == CustomerType.Individual ? 0 : 1;
                logMessage += ", customer type {CustomerType}";
                args.Add(customerType);
                requestUri += $"&customerType={customerTypeValue}";
            }

            logger.LogInformation(logMessage, args.ToArray());

            logger.LogInformation("Calling GET operation to {RequestUri}", requestUri);
            using var response = await client.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();

            return await stream.DeserializeAsync<SalesOrdersResult>(new JsonSerializerOptions
            {
                Converters =
                {
                    new JsonStringEnumConverter(),
                    new CustomerConverter<
                        Customer,
                        StoreCustomer,
                        IndividualCustomer>()
                },
                IgnoreReadOnlyProperties = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }

        public async Task<SalesOrder> GetSalesOrderAsync(string salesOrderNumber)
        {
            logger.LogInformation("Getting sales order with sales order number {SalesOrderNumber}", salesOrderNumber);
            var requestUri = $"SalesOrder/{salesOrderNumber}?&api-version=1.0";

            logger.LogInformation("Calling GET operation to {RequestUri}", client.BaseAddress + requestUri);
            using var response = await client.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();

            return await stream.DeserializeAsync<SalesOrder>(new JsonSerializerOptions
            {
                Converters =
                {
                    new JsonStringEnumConverter(),
                    new CustomerConverter<
                        Customer,
                        StoreCustomer,
                        IndividualCustomer>()
                },
                IgnoreReadOnlyProperties = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }

        public async Task<SalesOrder> UpdateSalesOrderAsync(SalesOrder salesOrder)
        {
            logger.LogInformation("Updating sales order with sales order number {SalesOrderNumber}", salesOrder.SalesOrderNumber);
            string requestUri = $"SalesOrder/{salesOrder.SalesOrderNumber}?&api-version=1.0";
            var options = new JsonSerializerOptions
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
            };

            string json = JsonSerializer.Serialize(salesOrder, options);
            logger.LogInformation("Calling PUT method on {RequestUri} with {JSON}", requestUri, json);

            using var response = await client.PutAsync(
                requestUri,
                new StringContent(json, Encoding.UTF8, "application/json")
            );
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();
            var updatedSalesOrder = await stream.DeserializeAsync<SalesOrder>(options);

            logger.LogInformation("Returning sales order {@SalesOrder}", updatedSalesOrder);
            return updatedSalesOrder;
        }

        public async Task ApproveSalesOrderAsync(string salesOrderNumber)
        {
            logger.LogInformation("Approving sales order with sales order number {SalesOrderNumber}", salesOrderNumber);
            string requestUri = $"SalesOrder/{salesOrderNumber}/approve?&api-version=1.0";

            logger.LogInformation("Calling PUT method on {RequestUri}", requestUri);

            using var request = new HttpRequestMessage(HttpMethod.Put, requestUri);
            request.Headers.Add("x-requestid", Guid.NewGuid().ToString());

            using var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            logger.LogInformation("Sales order {SalesOrderNumber} succesfully approved", salesOrderNumber);
        }
    }
}