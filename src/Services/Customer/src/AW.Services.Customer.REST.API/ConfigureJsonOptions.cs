using AW.SharedKernel.JsonConverters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace AW.Services.Customer.REST.API
{
    public class ConfigureJsonOptions : IConfigureOptions<JsonOptions>
    {
        private readonly CustomerConverter<
            Core.Models.GetCustomers.Customer,
            Core.Models.GetCustomers.StoreCustomer,
            Core.Models.GetCustomers.IndividualCustomer> _converterGetCustomers;

        private readonly CustomerConverter<
            Core.Models.GetCustomer.Customer,
            Core.Models.GetCustomer.StoreCustomer,
            Core.Models.GetCustomer.IndividualCustomer> _converterGetCustomer;

        private readonly CustomerConverter<
            Core.Models.UpdateCustomer.Customer,
            Core.Models.UpdateCustomer.StoreCustomer,
            Core.Models.UpdateCustomer.IndividualCustomer> _converterUpdateCustomer;

        public ConfigureJsonOptions(
            CustomerConverter<Core.Models.GetCustomers.Customer,
                Core.Models.GetCustomers.StoreCustomer,
                Core.Models.GetCustomers.IndividualCustomer> converterGetCustomers,
            CustomerConverter<Core.Models.GetCustomer.Customer,
                Core.Models.GetCustomer.StoreCustomer,
                Core.Models.GetCustomer.IndividualCustomer> converterGetCustomer,
            CustomerConverter<Core.Models.UpdateCustomer.Customer,
                Core.Models.UpdateCustomer.StoreCustomer,
                Core.Models.UpdateCustomer.IndividualCustomer> converterUpdateCustomer
        ) => (_converterGetCustomers, _converterGetCustomer, _converterUpdateCustomer) = 
                (converterGetCustomers, converterGetCustomer, converterUpdateCustomer);

        public void Configure(JsonOptions options)
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            options.JsonSerializerOptions.Converters.Add(_converterGetCustomers);
            options.JsonSerializerOptions.Converters.Add(_converterGetCustomer);
            options.JsonSerializerOptions.Converters.Add(_converterUpdateCustomer);
        }
    }
}