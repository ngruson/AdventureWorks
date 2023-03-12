using AW.SharedKernel.JsonConverters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace AW.Services.Sales.Order.REST.API
{
    public class ConfigureJsonOptions : IConfigureOptions<JsonOptions>
    {
        private readonly CustomerConverter<
            Core.Handlers.GetSalesOrder.Customer,
            Core.Handlers.GetSalesOrder.StoreCustomer,
            Core.Handlers.GetSalesOrder.IndividualCustomer> _customerConverter_getSalesOrder;

        private readonly CustomerConverter<
            Core.Handlers.UpdateSalesOrder.Customer,
            Core.Handlers.UpdateSalesOrder.StoreCustomer,
            Core.Handlers.UpdateSalesOrder.IndividualCustomer> _customerConverter_updateSalesOrder;

        public ConfigureJsonOptions(
            CustomerConverter<
                Core.Handlers.GetSalesOrder.Customer,
                Core.Handlers.GetSalesOrder.StoreCustomer,
                Core.Handlers.GetSalesOrder.IndividualCustomer> customerConverter_getSalesOrder,
            CustomerConverter<
                Core.Handlers.UpdateSalesOrder.Customer,
                Core.Handlers.UpdateSalesOrder.StoreCustomer,
                Core.Handlers.UpdateSalesOrder.IndividualCustomer> customerConverter_updateSalesOrder
        ) => (_customerConverter_getSalesOrder, _customerConverter_updateSalesOrder) = 
            (customerConverter_getSalesOrder, customerConverter_updateSalesOrder);

        public void Configure(JsonOptions options)
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            options.JsonSerializerOptions.Converters.Add(_customerConverter_getSalesOrder);
            options.JsonSerializerOptions.Converters.Add(_customerConverter_updateSalesOrder);
        }
    }
}
