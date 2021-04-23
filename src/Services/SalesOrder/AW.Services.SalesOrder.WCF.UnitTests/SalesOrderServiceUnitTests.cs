using AutoMapper;
using AW.Services.SalesOrder.Application.GetSalesOrder;
using AW.Services.SalesOrder.Application.GetSalesOrders;
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
                SalesOrders = new List<Application.GetSalesOrders.SalesOrderDto>
                {
                    new Application.GetSalesOrders.SalesOrderDto { SalesOrderNumber = "1" },
                    new Application.GetSalesOrders.SalesOrderDto { SalesOrderNumber = "2" }
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

            var dto = new Application.GetSalesOrder.SalesOrderDto
            {
                
            };

            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(x => x.Send(It.IsAny<GetSalesOrderQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(dto);
            var salesOrderService = new SalesOrderService(
                mockMediator.Object,
                mapper
            );

            //Act
            var request = new GetSalesOrderRequest();
            var result = await salesOrderService.GetSalesOrder(request);

            //Assert
            result.Should().NotBeNull();
            result.SalesOrder.Should().NotBeNull();
        }
    }
}