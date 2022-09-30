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
            Core.Models.Customer,
            Core.Models.StoreCustomer,
            Core.Models.IndividualCustomer> _converter;

        public ConfigureJsonOptions(
            CustomerConverter<
                Core.Models.Customer,
                Core.Models.StoreCustomer,
                Core.Models.IndividualCustomer> converter
        ) => _converter = converter;

        public void Configure(JsonOptions options)
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

            options.JsonSerializerOptions.Converters.Add(_converter);
        }
    }
}