using AutoFixture.Xunit2;
using AW.Services.Sales.Core.Handlers.GetSalesOrder;
using AW.Services.Sales.Core.Handlers.GetSalesOrders;
using salesOrdersForCustomers = AW.Services.Sales.Core.Handlers.GetSalesOrdersForCustomer;
using AW.Services.Sales.Order.REST.API.Controllers;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using AW.Services.Sales.Core.AutoMapper;

namespace AW.Services.Sales.Order.REST.API.UnitTests
{
    public class SalesOrderControllerUnitTests
    {
        public class GetSalesOrders
        {
            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task GetSalesOrders_ShouldReturnSalesOrders_WhenGivenSalesOrders(
            [Frozen] Mock<IMediator> mockMediator,
            List<Core.Handlers.GetSalesOrders.SalesOrderDto> salesOrders,
            [Greedy] SalesOrderController sut,
            GetSalesOrdersQuery query
        )
            {
                //Arrange
                var dto = new GetSalesOrdersDto
                {
                    SalesOrders = salesOrders,
                    TotalSalesOrders = salesOrders.Count
                };

                mockMediator.Setup(x => x.Send(
                    It.IsAny<GetSalesOrdersQuery>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(dto);

                //Act
                var actionResult = await sut.GetSalesOrders(query);

                //Assert
                var okObjectResult = actionResult as OkObjectResult;
                okObjectResult.Should().NotBeNull();

                var result = okObjectResult.Value as Core.Models.SalesOrdersResult;
                result.SalesOrders.Count.Should().Be(salesOrders.Count);
            }

            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task GetSalesOrders_ShouldReturnNotFound_WhenGivenNoSalesOrders(
                [Greedy] SalesOrderController sut,
                GetSalesOrdersQuery query
            )
            {
                //Act
                var actionResult = await sut.GetSalesOrders(query);

                //Assert
                var notFoundResult = actionResult as NotFoundResult;
                notFoundResult.Should().NotBeNull();
            }
        }

        public class GetSalesOrdersForCustomer
        {
            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task GetSalesOrdersForCustomer_ShouldReturnSalesOrders_WhenGivenSalesOrders(
                [Frozen] Mock<IMediator> mockMediator,
                List<salesOrdersForCustomers.SalesOrderDto> salesOrders,
                [Greedy] SalesOrderController sut,
                salesOrdersForCustomers.GetSalesOrdersForCustomerQuery query
            )
            {
                //Arrange
                var dto = new salesOrdersForCustomers.GetSalesOrdersDto
                {
                    SalesOrders = salesOrders,
                    TotalSalesOrders = salesOrders.Count
                };

                mockMediator.Setup(x => x.Send(
                    It.IsAny<salesOrdersForCustomers.GetSalesOrdersForCustomerQuery>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(dto);

                //Act
                var actionResult = await sut.GetSalesOrdersForCustomer(query);

                //Assert
                var okObjectResult = actionResult as OkObjectResult;
                okObjectResult.Should().NotBeNull();

                var result = okObjectResult.Value as Core.Models.SalesOrdersResult;
                result.SalesOrders.Count.Should().Be(salesOrders.Count);
            }

            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task GetSalesOrders_ShouldReturnNotFound_WhenGivenNoSalesOrders(
                [Greedy] SalesOrderController sut,
                GetSalesOrdersQuery query
            )
            {
                //Act
                var actionResult = await sut.GetSalesOrders(query);

                //Assert
                var notFoundResult = actionResult as NotFoundResult;
                notFoundResult.Should().NotBeNull();
            }
        }

        public class GetSalesOrder
        {
            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task GetSalesOrder_ShouldReturnSalesOrder_GivenSalesOrder(
            Core.Handlers.GetSalesOrder.SalesOrderDto salesOrder,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] SalesOrderController sut,
            GetSalesOrderQuery query
            )
            {
                //Arrange
                mockMediator.Setup(x => x.Send(It.IsAny<GetSalesOrderQuery>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(salesOrder);

                //Act
                var actionResult = await sut.GetSalesOrder(query);

                //Assert
                var okObjectResult = actionResult as OkObjectResult;
                okObjectResult.Should().NotBeNull();

                var result = okObjectResult.Value as Core.Models.SalesOrder;
                result.SalesOrderNumber.Should().Be(salesOrder.SalesOrderNumber);
            }

            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task GetSalesOrder_ShouldReturnNotFound_GivenNoSalesOrder(
                [Frozen] Mock<IMediator> mockMediator,
                [Greedy] SalesOrderController sut,
                GetSalesOrderQuery query
            )
            {
                //Arrange            
                mockMediator.Setup(x => x.Send(It.IsAny<GetSalesOrderQuery>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync((Core.Handlers.GetSalesOrder.SalesOrderDto)null);

                //Act
                var actionResult = await sut.GetSalesOrder(query);

                //Assert
                var notFoundResult = actionResult as NotFoundResult;
                notFoundResult.Should().NotBeNull();
            }
        }
    }
}