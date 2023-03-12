using System.Text.Json.Serialization;
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
                    Core.Handlers.GetSalesOrder.Customer,
                    Core.Handlers.GetSalesOrder.StoreCustomer,
                    Core.Handlers.GetSalesOrder.IndividualCustomer> converterGetSalesOrder,
                CustomerConverter<
                    Core.Handlers.UpdateSalesOrder.Customer,
                    Core.Handlers.UpdateSalesOrder.StoreCustomer,
                    Core.Handlers.UpdateSalesOrder.IndividualCustomer> converterUpdateSalesOrder,
                JsonOptions options)
            {
                //Arrange
                var sut = new ConfigureJsonOptions(
                      converterGetSalesOrder,
                      converterUpdateSalesOrder
                );

                //Act
                sut.Configure(options);

                //Assert
                options.JsonSerializerOptions.Converters[0].Should().BeOfType<JsonStringEnumConverter>();
                options.JsonSerializerOptions.Converters[1].Should().Be(converterGetSalesOrder);
                options.JsonSerializerOptions.Converters[2].Should().Be(converterUpdateSalesOrder);
            }
        }
    }
}
