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
using Xunit;
using AW.Services.Sales.Core.AutoMapper;
using AW.Services.Sales.Core.Handlers.ApproveSalesOrder;
using AW.Services.Sales.Core.Handlers.Identified;
using AW.Services.Sales.Core.Handlers.RejectSalesOrder;
using AW.Services.Sales.Core.Handlers.CancelSalesOrder;
using AW.Services.Sales.Core.Handlers.ShipSalesOrder;
using AW.Services.Sales.Core.Handlers.UpdateSalesOrder;
using AW.Services.Sales.Core.Handlers.DuplicateSalesOrder;
using AW.Services.Sales.Core.Exceptions;

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
                Core.Handlers.GetSalesOrders.IndividualCustomerDto customer,
                [Greedy] SalesOrderController sut,
                GetSalesOrdersQuery query
            )
            {
                //Arrange
                salesOrders.ForEach(_ => _.Customer = customer);

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
                okObjectResult!.Value.Should().Be(dto);
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
                salesOrdersForCustomers.IndividualCustomerDto customer,
                [Greedy] SalesOrderController sut,
                salesOrdersForCustomers.GetSalesOrdersForCustomerQuery query
            )
            {
                //Arrange
                salesOrders.ForEach(_ => _.Customer = customer);

                var dto = new salesOrdersForCustomers.GetSalesOrdersDto(
                    salesOrders,
                    salesOrders.Count
                );

                mockMediator.Setup(x => x.Send(
                    It.IsAny<salesOrdersForCustomers.GetSalesOrdersForCustomerQuery>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(dto);

                //Act
                var actionResult = await sut.GetSalesOrdersForCustomer(query);

                //Assert
                var okObjectResult = actionResult as OkObjectResult;
                okObjectResult!.Value.Should().Be(dto);
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
            Core.Handlers.GetSalesOrder.SalesOrder salesOrder,
            Core.Handlers.GetSalesOrder.IndividualCustomer customer,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] SalesOrderController sut,
            GetSalesOrderQuery query
            )
            {
                //Arrange
                salesOrder.Customer = customer;
                mockMediator.Setup(x => x.Send(It.IsAny<GetSalesOrderQuery>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(salesOrder);

                //Act
                var actionResult = await sut.GetSalesOrder(query);

                //Assert
                var okObjectResult = actionResult as OkObjectResult;
                okObjectResult.Should().NotBeNull();

                var result = okObjectResult?.Value as Core.Handlers.GetSalesOrder.SalesOrder;
                result?.SalesOrderNumber.Should().Be(salesOrder.SalesOrderNumber);
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
                    .ReturnsAsync((Core.Handlers.GetSalesOrder.SalesOrder?)null);

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
                Core.Handlers.UpdateSalesOrder.SalesOrder salesOrder,
                Core.Handlers.UpdateSalesOrder.IndividualCustomer customer
            )
            {
                //Arrange
                salesOrder.Customer = customer;

                mockMediator.Setup(x => x.Send(
                    It.IsAny<UpdateSalesOrderCommand>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(salesOrder);

                //Act
                var actionResult = await sut.UpdateSalesOrder(salesOrder.SalesOrderNumber!, salesOrder);

                //Assert
                var okResult = actionResult as OkObjectResult;
                okResult!.Value.Should().Be(salesOrder);
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

        public class DuplicateSalesOrder
        {
            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task DuplicateSalesOrder_Succeeds_ReturnOk(
                [Frozen] Mock<IMediator> mockMediator,
                [Greedy] SalesOrderController sut,
                DuplicateSalesOrderCommand command
            )
            {
                //Arrange

                //Act
                var actionResult = await sut.DuplicateSalesOrderAsync(command);

                //Assert
                actionResult.Should().BeOfType<OkResult>();

                mockMediator.Verify(_ => _.Send(
                        It.IsAny<DuplicateSalesOrderCommand>(),
                        It.IsAny<CancellationToken>()
                    )
                );
            }

            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task DuplicateSalesOrder_Fails_ReturnBadRequest(
                [Frozen] Mock<IMediator> mockMediator,
                [Greedy] SalesOrderController sut,
                DuplicateSalesOrderCommand command
            )
            {
                //Arrange
                mockMediator.Setup(_ => _.Send(
                    It.IsAny<DuplicateSalesOrderCommand>(),
                    It.IsAny<CancellationToken>()
                ))
                .ThrowsAsync(new DuplicateSalesOrderException(command.SalesOrderNumber));

                //Act
                var actionResult = await sut.DuplicateSalesOrderAsync(command);

                //Assert

                //Assert
                actionResult.Should().BeOfType<BadRequestResult>();
            }
        }
    }
}
