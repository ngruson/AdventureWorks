using System.Text.Json.Serialization;
using AW.Services.SharedKernel.JsonConverters;
using AW.SharedKernel.JsonConverters;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace AW.Services.Sales.Order.REST.API.UnitTests
{
    public  class ConfigureJsonOptionsUnitTests
    {
        public class Configure
        {
            [Theory, AutoMoqData]
            public void AddConverters(
                CustomerConverter<
                Core.Models.Customer,
                Core.Models.StoreCustomer,
                Core.Models.IndividualCustomer> converter,
                JsonOptions options)
            {
                //Arrange
                var sut = new ConfigureJsonOptions(
                      converter
                );

                //Act
                sut.Configure(options);

                //Assert
                options.JsonSerializerOptions.Converters[0].Should().BeOfType<JsonStringEnumConverter>();
                options.JsonSerializerOptions.Converters[1].Should().Be(converter);
            }
        }
    }
}
