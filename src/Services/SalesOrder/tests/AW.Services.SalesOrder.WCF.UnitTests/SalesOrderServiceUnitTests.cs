using AutoMapper;
using AW.Services.SalesOrder.Core.Handlers.GetSalesOrder;
using AW.Services.SalesOrder.Core.Handlers.GetSalesOrders;
using AW.Services.SalesOrder.WCF.Messages.GetSalesOrder;
using AW.Services.SalesOrder.WCF.Messages.ListSalesOrders;
using FluentAssertions;
using MediatR;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.SalesOrder.WCF.UnitTests
{
    public class SalesOrderServiceUnitTests
    {
        [Fact]
        public async Task ListSalesOrders_ReturnsSalesOrders()
        {
            //Arrange
            var mapper = new MapperConfiguration(opts =>
            {
                opts.AddProfile<MappingProfile>();
            }).CreateMapper();

            var dto = new GetSalesOrdersDto
            {
                SalesOrders = new List<Core.Handlers.GetSalesOrders.SalesOrderDto>
                {
                    new Core.Handlers.GetSalesOrders.SalesOrderDto { SalesOrderNumber = "1" },
                    new Core.Handlers.GetSalesOrders.SalesOrderDto { SalesOrderNumber = "2" }
                },
                TotalSalesOrders = 2
            };

            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(x => x.Send(It.IsAny<GetSalesOrdersQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(dto);
            var salesOrderService = new SalesOrderService(
                mockMediator.Object,
                mapper
            );

            //Act
            var request = new ListSalesOrdersRequest();
            var result = await salesOrderService.ListSalesOrders(request);

            //Assert
            result.Should().NotBeNull();
            result.SalesOrders.SalesOrder.Count().Should().Be(2);
            result.TotalSalesOrders.Should().Be(2);
        }

        [Fact]
        public async Task GetSalesOrder_ReturnSalesOrder()
        {
            //Arrange
            var mapper = new MapperConfiguration(opts =>
            {
                opts.AddProfile<MappingProfile>();
            }).CreateMapper();

            var dto = new Core.Handlers.GetSalesOrder.SalesOrderDto();

            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(x => x.Send(It.IsAny<GetSalesOrderQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(dto);
            var salesOrderService = new SalesOrderService(
                mockMediator.Object,
                mapper
            );

            //Act
            var request = new GetSalesOrderRequest
            {
                SalesOrderNumber = "SO43659"
            };
            var result = await salesOrderService.GetSalesOrder(request);

            //Assert
            result.Should().NotBeNull();
            result.SalesOrder.Should().NotBeNull();
        }
    }
}