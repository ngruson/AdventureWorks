using AutoMapper;
using AW.Services.SalesOrder.Application.GetSalesOrder;
using AW.Services.SalesOrder.Application.GetSalesOrders;
using AW.Services.SalesOrder.REST.API.Controllers;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.SalesOrder.REST.API.UnitTests
{
    public class SalesOrderControllerUnitTests
    {
        [Fact]
        public async Task GetSalesOrders_ShouldReturnSalesOrders_WhenGivenSalesOrders()
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

            var mockLogger = new Mock<ILogger<SalesOrderController>>();
            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(x => x.Send(It.IsAny<GetSalesOrdersQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(dto);

            var controller = new SalesOrderController(
                mockLogger.Object,
                mockMediator.Object,
                mapper
            );

            //Act
            var request = new GetSalesOrdersQuery();
            var actionResult = await controller.GetSalesOrders(request);

            //Assert
            var okObjectResult = actionResult as OkObjectResult;
            okObjectResult.Should().NotBeNull();

            var salesPersons = okObjectResult.Value as Models.SalesOrdersResult;
            salesPersons.SalesOrders.Count.Should().Be(2);
        }

        [Fact]
        public async Task GetSalesOrders_ShouldReturnNotFound_WhenGivenNoSalesOrders()
        {
            //Arrange
            var mapper = new MapperConfiguration(opts =>
            {
                opts.AddProfile<MappingProfile>();
            }).CreateMapper();            
            
            var mockLogger = new Mock<ILogger<SalesOrderController>>();
            var mockMediator = new Mock<IMediator>();

            var controller = new SalesOrderController(
                mockLogger.Object,
                mockMediator.Object,
                mapper
            );

            //Act
            var request = new GetSalesOrdersQuery();
            var actionResult = await controller.GetSalesOrders(request);

            //Assert
            var notFoundResult = actionResult as NotFoundResult;
            notFoundResult.Should().NotBeNull();
        }

        [Fact]
        public async Task GetSalesOrder_ShouldReturnSalesOrder_GivenSalesOrder()
        {
            //Arrange
            var mapper = new MapperConfiguration(opts =>
            {
                opts.AddProfile<MappingProfile>();
            }).CreateMapper();

            var dto = new Application.GetSalesOrder.SalesOrderDto { SalesOrderNumber = "1" };

            var mockLogger = new Mock<ILogger<SalesOrderController>>();
            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(x => x.Send(It.IsAny<GetSalesOrderQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(dto);

            var controller = new SalesOrderController(
                mockLogger.Object,
                mockMediator.Object,
                mapper
            );

            //Act
            var actionResult = await controller.GetSalesOrder("1");

            //Assert
            var okObjectResult = actionResult as OkObjectResult;
            okObjectResult.Should().NotBeNull();

            var salesPerson = okObjectResult.Value as Models.SalesOrder;
            salesPerson.Should().NotBeNull();
        }

        [Fact]
        public async Task GetSalesOrder_ShouldReturnNotFound_GivenNoSalesOrder()
        {
            //Arrange
            var mapper = new MapperConfiguration(opts =>
            {
                opts.AddProfile<MappingProfile>();
            }).CreateMapper();

            var mockLogger = new Mock<ILogger<SalesOrderController>>();
            var mockMediator = new Mock<IMediator>();

            var controller = new SalesOrderController(
                mockLogger.Object,
                mockMediator.Object,
                mapper
            );

            //Act
            var actionResult = await controller.GetSalesOrder("1");

            //Assert
            var notFoundResult = actionResult as NotFoundResult;
            notFoundResult.Should().NotBeNull();
        }
    }
}