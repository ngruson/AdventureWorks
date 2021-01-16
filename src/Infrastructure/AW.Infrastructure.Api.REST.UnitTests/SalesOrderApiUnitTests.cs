using AW.Core.Abstractions.Api.SalesOrderApi.GetSalesOrder;
using AW.Core.Abstractions.Api.SalesOrderApi.ListSalesOrders;
using AW.Core.Abstractions.Api.SalesPersonApi.GetSalesPerson;
using AW.Core.Abstractions.Api.SalesPersonApi.ListSalesPersons;
using AW.Infrastructure.Http;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Xunit;

namespace AW.Infrastructure.Api.REST.UnitTests
{
    public class SalesOrderApiUnitTests
    {
        [Fact]
        public async void ListSalesOrders_ReturnsSalesOrders()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<SalesOrderApi>>();
            var mockHttpRequestFactory = new Mock<IHttpRequestFactory>();
            mockHttpRequestFactory.Setup(x => x.Get(
                    It.IsAny<string>(), It.IsAny<Dictionary<string, string>>(), It.IsAny<string>()
                )
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new JsonContent(new ListSalesOrdersResponse
                {
                    SalesOrders = new List<Core.Abstractions.Api.SalesOrderApi.ListSalesOrders.SalesOrder>
                    {
                        new Core.Abstractions.Api.SalesOrderApi.ListSalesOrders.SalesOrder { SalesOrderNumber = "SO75123" },
                        new Core.Abstractions.Api.SalesOrderApi.ListSalesOrders.SalesOrder { SalesOrderNumber = "SO75124" }
                    }
                })
            });

            var baseAddress = "BaseAddress";

            var sut = new SalesOrderApi(
                mockLogger.Object,
                mockHttpRequestFactory.Object,
                baseAddress
            );

            //Act
            var response = await sut.ListSalesOrdersAsync(new ListSalesOrdersRequest());

            //Assert
            mockHttpRequestFactory.Verify(x => x.Get(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>(), It.IsAny<string>()));
            response.SalesOrders[0].SalesOrderNumber.Should().Be("SO75123");
            response.SalesOrders[1].SalesOrderNumber.Should().Be("SO75124");
        }

        [Fact]
        public async void GetSalesOrder_ReturnsSalesOrder()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<SalesOrderApi>>();
            var mockHttpRequestFactory = new Mock<IHttpRequestFactory>();
            mockHttpRequestFactory.Setup(x => x.Get(
                    It.IsAny<string>(), It.IsAny<string>()
                )
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new JsonContent(new GetSalesOrderResponse
                {
                    SalesOrder = new Core.Abstractions.Api.SalesOrderApi.GetSalesOrder.SalesOrder
                    {
                        SalesOrderNumber = "SO75123"
                    }
                })
            });

            var baseAddress = "BaseAddress";

            var sut = new SalesOrderApi(
                mockLogger.Object,
                mockHttpRequestFactory.Object,
                baseAddress
            );

            //Act
            var response = await sut.GetSalesOrderAsync(new GetSalesOrderRequest());

            //Assert
            mockHttpRequestFactory.Verify(x => x.Get(It.IsAny<string>(), It.IsAny<string>()));

            response.SalesOrder.SalesOrderNumber.Should().Be("SO75123");
        }
    }
}