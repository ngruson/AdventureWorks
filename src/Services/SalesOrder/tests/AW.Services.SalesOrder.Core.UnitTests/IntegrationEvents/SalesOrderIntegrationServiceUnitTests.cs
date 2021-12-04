using AutoFixture.Xunit2;
using AW.Services.SalesOrder.Core.IntegrationEvents;
using AW.Services.SalesOrder.Core.IntegrationEvents.Events;
using AW.SharedKernel.EventBus.Abstractions;
using AW.SharedKernel.EventBus.Events;
using AW.SharedKernel.EventBus.IntegrationEventLog;
using AW.SharedKernel.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.SalesOrder.Core.UnitTests.IntegrationEvents
{
    public class SalesOrderIntegrationServiceUnitTests
    {
        [Theory, AutoMoqData]
        public async Task AddAndSaveEventAsync_SaveEventAsyncIsCalled(
            [Frozen] Mock<IIntegrationEventLogService> mockEventLogService,
            SalesOrderIntegrationEventService sut,
            UserCheckoutAcceptedIntegrationEvent integrationEvent
        )
        {
            //Arrange

            //Act
            await sut.AddAndSaveEventAsync(integrationEvent);

            //Assert
            mockEventLogService.Verify(_ => _.SaveEventAsync(
                It.IsAny<IntegrationEvent>(),
                It.IsAny<Guid>()
            ));
        }

        [Theory, AutoMoqData]
        public async Task PublishEventsThroughEventBusAsync_PendingEvents_EventsArePublished(
            [Frozen] Mock<IIntegrationEventLogService> mockEventLogService,
            [Frozen] Mock<IEventBus> mockEventBus,
            SalesOrderIntegrationEventService sut,
            Guid transactionId,
            UserCheckoutAcceptedIntegrationEvent integrationEvent
        )
        {
            //Arrange
            var eventLogs = Enumerable.Repeat(
                    new IntegrationEventLogEntry(integrationEvent, transactionId), 3
                )
                .ToList();

            mockEventLogService.Setup(_ => _.RetrieveEventLogsPendingToPublishAsync(
                It.IsAny<Guid>()
            ))
            .ReturnsAsync(eventLogs);

            //Act
            await sut.PublishEventsThroughEventBusAsync(transactionId);

            //Assert
            mockEventLogService.Verify(_ => _.MarkEventAsInProgressAsync(It.IsAny<Guid>()),
                Times.Exactly(eventLogs.Count)
            );

            mockEventBus.Verify(_ => _.Publish(It.IsAny<IntegrationEvent>()),
                Times.Exactly(eventLogs.Count)
            );

            mockEventLogService.Verify(_ => _.MarkEventAsPublishedAsync(It.IsAny<Guid>()),
                Times.Exactly(eventLogs.Count)
            );
        }

        [Theory, AutoMoqData]
        public async Task PublishEventsThroughEventBusAsync_EventFails_EventIsMarkedAsFailed(
            [Frozen] Mock<IIntegrationEventLogService> mockEventLogService,
            [Frozen] Mock<IEventBus> mockEventBus,
            SalesOrderIntegrationEventService sut,
            Guid transactionId,
            UserCheckoutAcceptedIntegrationEvent integrationEvent
        )
        {
            //Arrange
            var eventLogs = Enumerable.Repeat(
                    new IntegrationEventLogEntry(integrationEvent, transactionId), 1
                )
                .ToList();

            mockEventLogService.Setup(_ => _.RetrieveEventLogsPendingToPublishAsync(
                It.IsAny<Guid>()
            ))
            .ReturnsAsync(eventLogs);

            mockEventBus.Setup(_ => _.Publish(It.IsAny<IntegrationEvent>()))
                .Throws<Exception>();

            //Act
            await sut.PublishEventsThroughEventBusAsync(transactionId);

            //Assert
            mockEventLogService.Verify(_ => _.MarkEventAsInProgressAsync(It.IsAny<Guid>()),
                Times.Exactly(eventLogs.Count)
            );

            mockEventBus.Verify(_ => _.Publish(It.IsAny<IntegrationEvent>()),
                Times.Exactly(eventLogs.Count)
            );

            mockEventLogService.Verify(_ => _.MarkEventAsFailedAsync(It.IsAny<Guid>()),
                Times.Exactly(eventLogs.Count)
            );
        }

        [Theory, AutoMoqData]
        public async Task PublishEventsThroughEventBusAsync_FirstEventFails_OtherEventsShouldBePublished(
            [Frozen] Mock<IIntegrationEventLogService> mockEventLogService,
            [Frozen] Mock<IEventBus> mockEventBus,
            SalesOrderIntegrationEventService sut,
            Guid transactionId,
            List<UserCheckoutAcceptedIntegrationEvent> integrationEvents
        )
        {
            //Arrange
            var eventLogs = integrationEvents.Select(e => new IntegrationEventLogEntry(
                    e, transactionId
                ))
                .ToList();
            
            eventLogs.ForEach(e => 
                e.DeserializeJsonContent(typeof(UserCheckoutAcceptedIntegrationEvent))
            );

            mockEventLogService.Setup(_ => _.RetrieveEventLogsPendingToPublishAsync(
                    It.IsAny<Guid>()
                ))
                .ReturnsAsync(eventLogs);

            mockEventBus.Setup(_ => _.Publish(It.Is<IntegrationEvent>(e =>
                    e.Id == integrationEvents[0].Id)
                ))
                .Throws<Exception>();

            //Act
            await sut.PublishEventsThroughEventBusAsync(transactionId);

            //Assert
            mockEventLogService.Verify(_ => _.MarkEventAsInProgressAsync(It.IsAny<Guid>()),
                Times.Exactly(eventLogs.Count)
            );

            mockEventBus.Verify(_ => _.Publish(It.IsAny<IntegrationEvent>()),
                Times.Exactly(eventLogs.Count)
            );

            mockEventLogService.Verify(_ => _.MarkEventAsPublishedAsync(It.IsAny<Guid>()),
                Times.Exactly(eventLogs.Count - 1)
            );

            mockEventLogService.Verify(_ => _.MarkEventAsFailedAsync(It.IsAny<Guid>()),
                Times.Once
            );
        }
    }
}