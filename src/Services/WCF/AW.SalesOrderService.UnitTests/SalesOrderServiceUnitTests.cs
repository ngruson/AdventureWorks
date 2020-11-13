using AutoMapper;
using AW.Application.SalesOrder.GetSalesOrder;
using AW.Application.SalesOrder.GetSalesOrders;
using AW.SalesOrderService.Messages.GetSalesOrder;
using AW.SalesOrderService.Messages.ListSalesOrders;
using FluentAssertions;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AW.SalesOrderService.UnitTests
{
    [TestClass]
    public class SalesOrderServiceUnitTests
    {
        [TestMethod]
        public async Task ListSalesOrders_ReturnsSalesOrders()
        {
            //Arrange
            var mapper = new MapperConfiguration(opts =>
            {
                opts.AddProfile<MappingProfile>();
            }).CreateMapper();

            var dto = new GetSalesOrdersDto
            {
                SalesOrders = new List<Application.SalesOrder.GetSalesOrders.SalesOrderDto>
                {
                    new Application.SalesOrder.GetSalesOrders.SalesOrderDto { SalesOrderNumber = "1" },
                    new Application.SalesOrder.GetSalesOrders.SalesOrderDto { SalesOrderNumber = "2" }
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

        [TestMethod]
        public async Task GetSalesOrder_ReturnSalesOrder()
        {
            //Arrange
            var mapper = new MapperConfiguration(opts =>
            {
                opts.AddProfile<MappingProfile>();
            }).CreateMapper();

            var dto = new Application.SalesOrder.GetSalesOrder.SalesOrderDto
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