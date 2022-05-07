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
using AW.Services.Sales.Core.Handlers.ApproveSalesOrder;
using System;
using AW.Services.Sales.Core.Handlers.Identified;
using AW.Services.Sales.Core.Handlers.RejectSalesOrder;
using AW.Services.Sales.Core.Handlers.CancelSalesOrder;
using AW.Services.Sales.Core.Handlers.ShipSalesOrder;
using AW.Services.Sales.Core.Handlers.UpdateSalesOrder;

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

        public class UpdateSalesOrder
        {
            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task UpdateSalesOrder_ShouldReturnSalesOrder_GivenSalesOrder(
                [Frozen] Mock<IMediator> mockMediator,
                [Greedy] SalesOrderController sut,
                Core.Handlers.UpdateSalesOrder.SalesOrderDto dto,
                Core.Models.SalesOrder salesOrder
            )
            {
                //Arrange
                mockMediator.Setup(x => x.Send(
                    It.IsAny<UpdateSalesOrderCommand>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(dto);

                //Act
                var actionResult = await sut.UpdateSalesOrder(salesOrder.SalesOrderNumber, salesOrder);

                //Assert
                var okResult = actionResult as OkObjectResult;
                okResult.Should().NotBeNull();

                var updatedSalesOrder = okResult.Value as Core.Models.SalesOrder;
                updatedSalesOrder.Should().NotBeNull();
            }
        }

        public class ApproveSalesOrder
        {
            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task ApproveSalesOrder_ApproveSucceeds_ReturnOk(
                [Frozen] Mock<IMediator> mockMediator,
                [Greedy] SalesOrderController sut,
                ApproveSalesOrderCommand command
            )
            {
                //Arrange
                string requestId = Guid.NewGuid().ToString();

                mockMediator.Setup(x => x.Send(
                    It.IsAny<IdentifiedCommand<ApproveSalesOrderCommand, bool>>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(true);

                //Act
                var actionResult = await sut.ApproveSalesOrderAsync(command, requestId);

                //Assert
                actionResult.Should().BeOfType<OkResult>();

                mockMediator.Verify(_ => _.Send(
                        It.IsAny<IdentifiedCommand<ApproveSalesOrderCommand, bool>>(),
                        It.IsAny<CancellationToken>()
                    )
                );
            }

            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task ApproveSalesOrder_ApproveDoesNotSucceed_ReturnBadRequest(
                [Greedy] SalesOrderController sut,
                ApproveSalesOrderCommand command
            )
            {
                //Arrange
                string requestId = Guid.NewGuid().ToString();

                //Act
                var actionResult = await sut.ApproveSalesOrderAsync(command, requestId);

                //Assert
                actionResult.Should().BeOfType<BadRequestResult>();
            }

            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task ApproveSalesOrder_InvalidRequestId_ReturnBadRequest(
                [Greedy] SalesOrderController sut,
                ApproveSalesOrderCommand command,
                string requestId
            )
            {
                //Arrange

                //Act
                var actionResult = await sut.ApproveSalesOrderAsync(command, requestId);

                //Assert
                actionResult.Should().BeOfType<BadRequestResult>();
            }
        }

        public class RejectSalesOrder
        {
            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task RejectSalesOrder_RejectSucceeds_ReturnOk(
                [Frozen] Mock<IMediator> mockMediator,
                [Greedy] SalesOrderController sut,
                RejectSalesOrderCommand command
            )
            {
                //Arrange
                string requestId = Guid.NewGuid().ToString();

                mockMediator.Setup(x => x.Send(
                    It.IsAny<IdentifiedCommand<RejectSalesOrderCommand, bool>>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(true);

                //Act
                var actionResult = await sut.RejectSalesOrderAsync(command, requestId);

                //Assert
                actionResult.Should().BeOfType<OkResult>();

                mockMediator.Verify(_ => _.Send(
                        It.IsAny<IdentifiedCommand<RejectSalesOrderCommand, bool>>(),
                        It.IsAny<CancellationToken>()
                    )
                );
            }

            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task RejectSalesOrder_RejectDoesNotSucceed_ReturnBadRequest(
                [Greedy] SalesOrderController sut,
                RejectSalesOrderCommand command
            )
            {
                //Arrange
                string requestId = Guid.NewGuid().ToString();

                //Act
                var actionResult = await sut.RejectSalesOrderAsync(command, requestId);

                //Assert
                actionResult.Should().BeOfType<BadRequestResult>();
            }

            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task RejectSalesOrder_InvalidRequestId_ReturnBadRequest(
                [Greedy] SalesOrderController sut,
                RejectSalesOrderCommand command,
                string requestId
            )
            {
                //Arrange

                //Act
                var actionResult = await sut.RejectSalesOrderAsync(command, requestId);

                //Assert
                actionResult.Should().BeOfType<BadRequestResult>();
            }
        }

        public class CancelSalesOrder
        {
            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task CancelSalesOrder_CancelSucceeds_ReturnOk(
                [Frozen] Mock<IMediator> mockMediator,
                [Greedy] SalesOrderController sut,
                CancelSalesOrderCommand command
            )
            {
                //Arrange
                string requestId = Guid.NewGuid().ToString();

                mockMediator.Setup(x => x.Send(
                    It.IsAny<IdentifiedCommand<CancelSalesOrderCommand, bool>>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(true);

                //Act
                var actionResult = await sut.CancelSalesOrderAsync(command, requestId);

                //Assert
                actionResult.Should().BeOfType<OkResult>();

                mockMediator.Verify(_ => _.Send(
                        It.IsAny<IdentifiedCommand<CancelSalesOrderCommand, bool>>(),
                        It.IsAny<CancellationToken>()
                    )
                );
            }

            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task CancelSalesOrder_CancelDoesNotSucceed_ReturnBadRequest(
                [Greedy] SalesOrderController sut,
                CancelSalesOrderCommand command
            )
            {
                //Arrange
                string requestId = Guid.NewGuid().ToString();

                //Act
                var actionResult = await sut.CancelSalesOrderAsync(command, requestId);

                //Assert
                actionResult.Should().BeOfType<BadRequestResult>();
            }

            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task CancelSalesOrder_InvalidRequestId_ReturnBadRequest(
                [Greedy] SalesOrderController sut,
                CancelSalesOrderCommand command,
                string requestId
            )
            {
                //Arrange

                //Act
                var actionResult = await sut.CancelSalesOrderAsync(command, requestId);

                //Assert
                actionResult.Should().BeOfType<BadRequestResult>();
            }
        }

        public class ShipSalesOrder
        {
            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task ShipSalesOrder_ShipSucceeds_ReturnOk(
                [Frozen] Mock<IMediator> mockMediator,
                [Greedy] SalesOrderController sut,
                ShipSalesOrderCommand command
            )
            {
                //Arrange
                string requestId = Guid.NewGuid().ToString();

                mockMediator.Setup(x => x.Send(
                    It.IsAny<IdentifiedCommand<ShipSalesOrderCommand, bool>>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(true);

                //Act
                var actionResult = await sut.ShipSalesOrderAsync(command, requestId);

                //Assert
                actionResult.Should().BeOfType<OkResult>();

                mockMediator.Verify(_ => _.Send(
                        It.IsAny<IdentifiedCommand<ShipSalesOrderCommand, bool>>(),
                        It.IsAny<CancellationToken>()
                    )
                );
            }

            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task ShipSalesOrder_ShipDoesNotSucceed_ReturnBadRequest(
                [Greedy] SalesOrderController sut,
                ShipSalesOrderCommand command
            )
            {
                //Arrange
                string requestId = Guid.NewGuid().ToString();

                //Act
                var actionResult = await sut.ShipSalesOrderAsync(command, requestId);

                //Assert
                actionResult.Should().BeOfType<BadRequestResult>();
            }

            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task ShipSalesOrder_InvalidRequestId_ReturnBadRequest(
                [Greedy] SalesOrderController sut,
                ShipSalesOrderCommand command,
                string requestId
            )
            {
                //Arrange

                //Act
                var actionResult = await sut.ShipSalesOrderAsync(command, requestId);

                //Assert
                actionResult.Should().BeOfType<BadRequestResult>();
            }
        }
    }
}