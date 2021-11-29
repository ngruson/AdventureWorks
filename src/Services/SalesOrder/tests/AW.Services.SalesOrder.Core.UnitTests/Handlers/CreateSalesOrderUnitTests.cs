using AutoFixture.Xunit2;
using AW.Services.SalesOrder.Core.Handlers.CreateSalesOrder;
using AW.Services.SalesOrder.Core.IntegrationEvents;
using AW.SharedKernel.EventBus.Events;
using AW.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            [Frozen] Mock<IUnitOfWork> unitOfWorkMock,
            [Frozen] Mock<IRepository<Core.Entities.SalesOrder>> salesOrderRepositoryMock,
            CreateSalesOrderCommandHandler sut,
            CreateSalesOrderCommand command
        )
        {
            //Arrange
            salesOrderRepositoryMock.Setup(_ => _.UnitOfWork)
                .Returns(unitOfWorkMock.Object);

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

            unitOfWorkMock.Verify(_ => _.SaveEntitiesAsync(It.IsAny<CancellationToken>()));
        }
    }
}