using AW.SharedKernel.JsonConverters;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using GetSalesOrders = AW.UI.Web.SharedKernel.SalesOrder.Handlers.GetSalesOrders;
using GetSalesOrder = AW.UI.Web.SharedKernel.SalesOrder.Handlers.GetSalesOrder;
using UpdateSalesOrder = AW.UI.Web.SharedKernel.SalesOrder.Handlers.UpdateSalesOrder;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AW.UI.Web.Infrastructure.ApiClients
{
    public class SalesOrderApiClient : ISalesOrderApiClient
    {
        private readonly ILogger<SalesOrderApiClient?> _logger;
        private readonly HttpClient _httpClient;

        private readonly CustomerConverter<
            GetSalesOrders.Customer,
            GetSalesOrders.StoreCustomer,
            GetSalesOrders.IndividualCustomer> _converterGetSalesOrders;

        private readonly CustomerConverter<
            GetSalesOrder.Customer,
            GetSalesOrder.StoreCustomer,
            GetSalesOrder.IndividualCustomer> _converterGetSalesOrder;

        private readonly CustomerConverter<
            UpdateSalesOrder.Customer,
            UpdateSalesOrder.StoreCustomer,
            UpdateSalesOrder.IndividualCustomer> _converterUpdateSalesOrder;

        public SalesOrderApiClient(
            ILogger<SalesOrderApiClient?> logger, 
            HttpClient httpClient,
            CustomerConverter<GetSalesOrders.Customer, GetSalesOrders.StoreCustomer, GetSalesOrders.IndividualCustomer> converterGetSalesOrders,
            CustomerConverter<GetSalesOrder.Customer, GetSalesOrder.StoreCustomer, GetSalesOrder.IndividualCustomer> converterGetSalesOrder,
            CustomerConverter<UpdateSalesOrder.Customer, UpdateSalesOrder.StoreCustomer, UpdateSalesOrder.IndividualCustomer> converterUpdateSalesOrder
        ) => (_httpClient, _logger, _converterGetSalesOrders, _converterGetSalesOrder, _converterUpdateSalesOrder) = 
                (httpClient, logger, converterGetSalesOrders, converterGetSalesOrder, converterUpdateSalesOrder);

        public async Task<GetSalesOrders.SalesOrdersResult?> GetSalesOrdersAsync(int pageIndex, int pageSize, string? territory, GetSalesOrders.CustomerType? customerType)
        {
            string requestUri = $"/salesorder-api/SalesOrder?&api-version=1.0&pageIndex={pageIndex}&pageSize={pageSize}";
            string logMessage = "Getting sales orders with page index {PageIndex}, page size {PageSize}";

            List<object> args = new() { pageIndex, pageSize };

            if (!string.IsNullOrEmpty(territory))
            {
                logMessage += ", territory {Territory}";
                args.Add(territory);
                requestUri += $"&territory={territory}";
            }
            if (customerType.HasValue)
            {
                var customerTypeValue = customerType.Value == GetSalesOrders.CustomerType.Individual ? 0 : 1;
                logMessage += ", customer type {CustomerType}";
                args.Add(customerType);
                requestUri += $"&customerType={customerTypeValue}";
            }

            _logger.LogInformation(logMessage, args.ToArray());

            _logger.LogInformation("Calling GET operation to {RequestUri}", requestUri);
            using var response = await _httpClient.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();

            return await stream.DeserializeAsync<GetSalesOrders.SalesOrdersResult?>(new JsonSerializerOptions
            {
                Converters =
                {
                    new JsonStringEnumConverter(),
                    _converterGetSalesOrders
                },
                IgnoreReadOnlyProperties = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }

        public async Task<GetSalesOrder.SalesOrder?> GetSalesOrderAsync(string? salesOrderNumber)
        {
            _logger.LogInformation("Getting sales order with sales order number {SalesOrderNumber}", salesOrderNumber);
            var requestUri = $"SalesOrder/{salesOrderNumber}?&api-version=1.0";

            _logger.LogInformation("Calling GET operation to {RequestUri}", _httpClient.BaseAddress + requestUri);
            using var response = await _httpClient.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();

            return await stream.DeserializeAsync<GetSalesOrder.SalesOrder?>(new JsonSerializerOptions
            {
                Converters =
                {
                    new JsonStringEnumConverter(),
                    _converterGetSalesOrder
                },
                IgnoreReadOnlyProperties = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }

        public async Task<UpdateSalesOrder.SalesOrder?> UpdateSalesOrderAsync(UpdateSalesOrder.SalesOrder salesOrder)
        {
            _logger.LogInformation("Updating sales order with sales order number {SalesOrderNumber}", salesOrder.SalesOrderNumber);
            string requestUri = $"SalesOrder/{salesOrder.SalesOrderNumber}?&api-version=1.0";
            var options = new JsonSerializerOptions
            {
                Converters =
                {
                    new JsonStringEnumConverter(),
                    _converterUpdateSalesOrder
                },
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            string json = JsonSerializer.Serialize(salesOrder, options);
            _logger.LogInformation("Calling PUT method on {RequestUri} with {JSON}", requestUri, json);

            using var response = await _httpClient.PutAsync(
                requestUri,
                new StringContent(json, Encoding.UTF8, "application/json")
            );
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();
            var updatedSalesOrder = await stream.DeserializeAsync<UpdateSalesOrder.SalesOrder?>(options);

            _logger.LogInformation("Returning sales order {@SalesOrder}", updatedSalesOrder);
            return updatedSalesOrder;
        }

        public async Task ApproveSalesOrderAsync(string salesOrderNumber)
        {
            _logger.LogInformation("Approving sales order with sales order number {SalesOrderNumber}", salesOrderNumber);
            string requestUri = $"SalesOrder/{salesOrderNumber}/approve?&api-version=1.0";

            _logger.LogInformation("Calling PUT method on {RequestUri}", requestUri);

            using var request = new HttpRequestMessage(HttpMethod.Put, requestUri);
            request.Headers.Add("x-requestid", Guid.NewGuid().ToString());

            using var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            _logger.LogInformation("Sales order {SalesOrderNumber} succesfully approved", salesOrderNumber);
        }

        public async Task DuplicateSalesOrderAsync(string salesOrderNumber)
        {
            _logger.LogInformation("Duplicating sales order with sales order number {SalesOrderNumber}", salesOrderNumber);
            string requestUri = $"SalesOrder/{salesOrderNumber}/duplicate?&api-version=1.0";

            _logger.LogInformation("Calling POST method on {RequestUri}", requestUri);

            using var request = new HttpRequestMessage(HttpMethod.Post, requestUri);
            request.Headers.Add("x-requestid", Guid.NewGuid().ToString());

            using var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            _logger.LogInformation("Sales order {SalesOrderNumber} succesfully duplicated", salesOrderNumber);
        }

        public async Task DeleteSalesOrderAsync(string salesOrderNumber)
        {
            _logger.LogInformation("Deleting sales order with sales order number {SalesOrderNumber}", salesOrderNumber);
            string requestUri = $"SalesOrder/{salesOrderNumber}?&api-version=1.0";

            _logger.LogInformation("Calling DELETE method on {RequestUri}", requestUri);

            using var request = new HttpRequestMessage(HttpMethod.Delete, requestUri);
            request.Headers.Add("x-requestid", Guid.NewGuid().ToString());

            using var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            _logger.LogInformation("Sales order {SalesOrderNumber} succesfully deleted", salesOrderNumber);
        }
    }
}