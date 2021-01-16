using AutoMapper;
using AW.Core.Abstractions.Api.SalesOrderApi.GetSalesOrder;
using AW.Core.Abstractions.Api.SalesOrderApi.ListSalesOrders;
using AW.Infrastructure.Api.WCF.AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace AW.Infrastructure.Api.WCF.UnitTests
{
    public class SalesOrderServiceWCFUnitTests
    {
        [Fact]
        public async void ListSalesOrders_ReturnsSalesOrders()
        {
            //Arrange
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<SalesOrderProfile>())
                .CreateMapper();
            var mockLogger = new Mock<ILogger<SalesOrderServiceWCF>>();
            var mockSalesOrderService = new Mock<SalesOrderService.ISalesOrderService>();
            mockSalesOrderService.Setup(x => x.ListSalesOrdersAsync(It.IsAny<SalesOrderService.ListSalesOrdersRequest>()))
                .ReturnsAsync(new SalesOrderService.ListSalesOrdersResponseListSalesOrdersResult
                {
                    TotalSalesOrders = 2,
                    SalesOrders = new SalesOrderService.SalesOrder[]
                    {
                        new SalesOrderService.SalesOrder
                        {
                            SalesOrderNumber = "1"
                        },
                        new SalesOrderService.SalesOrder
                        {
                            SalesOrderNumber = "2"
                        }
                    }
                });

            var sut = new SalesOrderServiceWCF(
                mockLogger.Object,
                mapper,
                mockSalesOrderService.Object
            );

            //Act
            var response = await sut.ListSalesOrdersAsync(new ListSalesOrdersRequest());

            //Assert
            mockSalesOrderService.Verify(x => x.ListSalesOrdersAsync(It.IsAny<SalesOrderService.ListSalesOrdersRequest>()));
            response.SalesOrders[0].SalesOrderNumber.Should().Be("1");
            response.SalesOrders[1].SalesOrderNumber.Should().Be("2");
        }

        [Fact]
        public async void GetSalesOrder_ReturnsSalesOrder()
        {
            //Arrange
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<SalesOrderProfile>())
                .CreateMapper();
            var mockLogger = new Mock<ILogger<SalesOrderServiceWCF>>();
            var mockSalesOrderService = new Mock<SalesOrderService.ISalesOrderService>();
            mockSalesOrderService.Setup(x => x.GetSalesOrderAsync(It.IsAny<SalesOrderService.GetSalesOrderRequest>()))
                .ReturnsAsync(new SalesOrderService.GetSalesOrderResponseGetSalesOrderResult
                {
                    SalesOrder = new SalesOrderService.SalesOrder1
                    {
                        SalesOrderNumber = "1"
                    }
                });

            var sut = new SalesOrderServiceWCF(
                mockLogger.Object,
                mapper,
                mockSalesOrderService.Object
            );

            //Act
            var response = await sut.GetSalesOrderAsync(new GetSalesOrderRequest());

            //Assert
            mockSalesOrderService.Verify(x => x.GetSalesOrderAsync(It.IsAny<SalesOrderService.GetSalesOrderRequest>()));
            response.SalesOrder.SalesOrderNumber.Should().Be("1");
        }
    }
}