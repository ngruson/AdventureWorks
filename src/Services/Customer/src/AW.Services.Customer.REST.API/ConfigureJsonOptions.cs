using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text.Json.Serialization;
using System.Text.Json;
using AW.Services.SharedKernel.JsonConverters;
using AW.SharedKernel.JsonConverters;

namespace AW.Services.Customer.REST.API
{
    public class ConfigureJsonOptions : IConfigureOptions<JsonOptions>
    {
        private readonly CustomerConverter<
            Core.Handlers.GetCustomers.Customer,
            Core.Handlers.GetCustomers.StoreCustomer,
            Core.Handlers.GetCustomers.IndividualCustomer> _converterGetCustomers;

        private readonly CustomerConverter<
            Core.Handlers.GetCustomer.Customer,
            Core.Handlers.GetCustomer.StoreCustomer,
            Core.Handlers.GetCustomer.IndividualCustomer> _converterGetCustomer;

        private readonly CustomerConverter<
            Core.Handlers.UpdateCustomer.Customer,
            Core.Handlers.UpdateCustomer.StoreCustomer,
            Core.Handlers.UpdateCustomer.IndividualCustomer> _converterUpdateCustomer;

        private readonly EmailAddressConverter _emailAddressConverter;

        public ConfigureJsonOptions(
            CustomerConverter<
                Core.Handlers.GetCustomers.Customer,
                Core.Handlers.GetCustomers.StoreCustomer,
                Core.Handlers.GetCustomers.IndividualCustomer
            > converterGetCustomers,
            CustomerConverter<
                Core.Handlers.GetCustomer.Customer,
                Core.Handlers.GetCustomer.StoreCustomer,
                Core.Handlers.GetCustomer.IndividualCustomer
            > converterGetCustomer,
            CustomerConverter<
                Core.Handlers.UpdateCustomer.Customer,
                Core.Handlers.UpdateCustomer.StoreCustomer,
                Core.Handlers.UpdateCustomer.IndividualCustomer
            > converterUpdateCustomer,
            EmailAddressConverter emailAddressConverter)
        {
            _converterGetCustomers = converterGetCustomers;
            _converterGetCustomer = converterGetCustomer;
            _converterUpdateCustomer = converterUpdateCustomer;
            _emailAddressConverter = emailAddressConverter;
        }
                

        public void Configure(JsonOptions options)
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            options.JsonSerializerOptions.Converters.Add(_converterGetCustomers);
            options.JsonSerializerOptions.Converters.Add(_converterGetCustomer);
            options.JsonSerializerOptions.Converters.Add(_converterUpdateCustomer);
            options.JsonSerializerOptions.Converters.Add(_emailAddressConverter);
        }
    }
}
