using AW.SharedKernel.JsonConverters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text.Json.Serialization;
using System.Text.Json;
using AW.Services.SharedKernel.JsonConverters;

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

        private readonly EmailAddressConverter _emailAddressConverter;

        public ConfigureJsonOptions(
            CustomerConverter<Core.Models.GetCustomers.Customer,
                Core.Models.GetCustomers.StoreCustomer,
                Core.Models.GetCustomers.IndividualCustomer> converterGetCustomers,
            CustomerConverter<Core.Models.GetCustomer.Customer,
                Core.Models.GetCustomer.StoreCustomer,
                Core.Models.GetCustomer.IndividualCustomer> converterGetCustomer,
            CustomerConverter<Core.Models.UpdateCustomer.Customer,
                Core.Models.UpdateCustomer.StoreCustomer,
                Core.Models.UpdateCustomer.IndividualCustomer> converterUpdateCustomer,
            EmailAddressConverter emailAddressConverter
        ) => (_converterGetCustomers, _converterGetCustomer, _converterUpdateCustomer, _emailAddressConverter) = 
                (converterGetCustomers, converterGetCustomer, converterUpdateCustomer, emailAddressConverter);

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
