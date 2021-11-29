using AutoFixture.Xunit2;
using AW.Services.SalesOrder.Core.Handlers.CreateSalesOrder;
using AW.Services.SalesOrder.Core.IntegrationEvents;
using AW.SharedKernel.EventBus.Events;
using AW.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.SalesOrder.Core.UnitTests.Handlers
{
    public class CreateSalesOrderUnitTests
    {
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task Handle_SalesOrderExists_ReturnSalesOrder(
            [Frozen] Mock<ISalesOrderIntegrationEventService> salesOrderIntegrationEventServiceMock,
            [Frozen] Mock<IRepository<Core.Entities.SalesOrder>> salesOrderRepositoryMock,
            CreateSalesOrderCommandHandler sut,
            CreateSalesOrderCommand command
        )
        {
            //Arrange

            //Act
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
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