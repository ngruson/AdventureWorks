using System.Text.Json.Serialization;
using AW.Services.SharedKernel.JsonConverters;
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
                EmailAddressConverter emailAddressConverter,
                JsonOptions options)
            {
                //Arrange
                var sut = new ConfigureJsonOptions(
                      emailAddressConverter
                );

                //Act
                sut.Configure(options);

                //Assert
                options.JsonSerializerOptions.Converters[0].Should().BeOfType<JsonStringEnumConverter>();
                options.JsonSerializerOptions.Converters[4].Should().Be(emailAddressConverter);
            }
        }
    }
}
