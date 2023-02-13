using System.Text.Json.Serialization;
using AW.Services.SharedKernel.JsonConverters;
using AW.SharedKernel.JsonConverters;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace AW.Services.Customer.REST.API.UnitTests
{
    public class ConfigureJsonOptionsUnitTests
    {
        public class Configure
        {
            [Theory, AutoMoqData]
            public void AddConverters(
                CustomerConverter<Core.Models.GetCustomers.Customer,
                    Core.Models.GetCustomers.StoreCustomer,
                    Core.Models.GetCustomers.IndividualCustomer> converterGetCustomers,
                CustomerConverter<Core.Models.GetCustomer.Customer,
                    Core.Models.GetCustomer.StoreCustomer,
                    Core.Models.GetCustomer.IndividualCustomer> converterGetCustomer,
                CustomerConverter<Core.Models.UpdateCustomer.Customer,
                    Core.Models.UpdateCustomer.StoreCustomer,
                    Core.Models.UpdateCustomer.IndividualCustomer> converterUpdateCustomer,
                EmailAddressConverter emailAddressConverter,
                JsonOptions options)
            {
                //Arrange
                var sut = new ConfigureJsonOptions(
                      converterGetCustomers,
                      converterGetCustomer,
                      converterUpdateCustomer,
                      emailAddressConverter
                );

                //Act
                sut.Configure(options);

                //Assert
                options.JsonSerializerOptions.Converters[0].Should().BeOfType<JsonStringEnumConverter>();
                options.JsonSerializerOptions.Converters[1].Should().Be(converterGetCustomers);
                options.JsonSerializerOptions.Converters[2].Should().Be(converterGetCustomer);
                options.JsonSerializerOptions.Converters[3].Should().Be(converterUpdateCustomer);
                options.JsonSerializerOptions.Converters[4].Should().Be(emailAddressConverter);
            }
        }
    }
}
