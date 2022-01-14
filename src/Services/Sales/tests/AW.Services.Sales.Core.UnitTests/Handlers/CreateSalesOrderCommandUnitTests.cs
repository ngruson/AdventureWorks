using AutoFixture.Xunit2;
using AW.Services.Sales.Core.Handlers.CreateSalesOrder;
using AW.Services.Sales.Core.IntegrationEvents;
using AW.Services.Sales.Core.Specifications;
using AW.SharedKernel.EventBus.Events;
using AW.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.Sales.Core.UnitTests.Handlers
{
    public class CreateSalesOrderCommandUnitTests
    {
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task Handle_AddressExists_CreateSalesOrder(
            [Frozen] Mock<ISalesOrderIntegrationEventService> salesOrderIntegrationEventServiceMock,
            [Frozen] Mock<IRepository<Core.Entities.SalesOrder>> salesOrderRepositoryMock,
            CreateSalesOrderCommandHandler sut,
            CreateSalesOrderCommand command
        )
        {
            //Arrange
            command.OrderItems.ForEach(_ => _.Discount = 0);

            //Act
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.Should().BeTrue();
            salesOrderIntegrationEventServiceMock.Verify(_ => _.AddAndSaveEventAsync(
                It.IsAny<IntegrationEvent>())
            );

            salesOrderRepositoryMock.Verify(_ => _.AddAsync(
                It.IsAny<Core.Entities.SalesOrder>(),
                It.IsAny<CancellationToken>())
            );
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task Handle_AddressDoesNotExist_CreateSalesOrder(
            [Frozen] Mock<ISalesOrderIntegrationEventService> salesOrderIntegrationEventServiceMock,
            [Frozen] Mock<IRepository<Core.Entities.SalesOrder>> salesOrderRepositoryMock,
            [Frozen] Mock<IRepository<Core.Entities.Address>> addressRepositoryMock,
            CreateSalesOrderCommandHandler sut,
            CreateSalesOrderCommand command
        )
        {
            //Arrange
            command.OrderItems.ForEach(_ => _.Discount = 0);

            addressRepositoryMock.Setup(_ => _.GetBySpecAsync(
                It.IsAny<GetAddressSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync((Core.Entities.Address)null);

            //Act
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.Should().BeTrue();
            salesOrderIntegrationEventServiceMock.Verify(_ => _.AddAndSaveEventAsync(
                It.IsAny<IntegrationEvent>())
            );

            salesOrderRepositoryMock.Verify(_ => _.AddAsync(
                It.IsAny<Core.Entities.SalesOrder>(),
                It.IsAny<CancellationToken>())
            );
        }
    }
}