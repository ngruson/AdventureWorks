using AutoFixture.Xunit2;
using AW.Services.Infrastructure.EventBus;
using AW.Services.Infrastructure.EventBus.AzureServiceBus;
using AW.SharedKernel.UnitTesting;
using Azure;
using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using sb = AW.Services.Infrastructure.EventBus.AzureServiceBus;

namespace AW.Services.Infrastructure.EventBus.UnitTests.EventBusServiceBus
{
    public class EventBusServiceBusUnitTests
    {
        public class Publish
        {
            [Theory, AutoMoqData]
            public void Publish_OK(
                [Frozen] Mock<IServiceBusPersisterConnection> mockConnection,
                [Frozen] Mock<ServiceBusClient> mockServiceBusClient,
                [Frozen] Mock<ServiceBusSender> mockServiceBusSender,
                sb.EventBusServiceBus sut,
                TestIntegrationEvent integrationEvent
            )
            {
                //Arrange            
                mockServiceBusSender.Setup(_ => _.SendMessageAsync(
                    It.IsAny<ServiceBusMessage>(),
                    It.IsAny<CancellationToken>()
                ))
                .Returns(Task.CompletedTask);

                mockServiceBusClient.Setup(_ => _.CreateSender(It.IsAny<string>()))
                    .Returns(mockServiceBusSender.Object);
                mockConnection.Setup(_ => _.ServiceBusClient)
                    .Returns(mockServiceBusClient.Object);

                //Act
                sut.Publish(integrationEvent);

                //Assert
                mockServiceBusSender.Verify(_ => _.SendMessageAsync(
                    It.IsAny<ServiceBusMessage>(),
                    It.IsAny<CancellationToken>()
                ));
            }
        }

        public class SubscribeDynamic
        {
            [Theory, AutoMoqData]
            public void SubscribeDynamic_OK(
                [Frozen] Mock<IEventBusSubscriptionsManager> mockSubsManager,
                sb.EventBusServiceBus sut
            )
            {
                //Arrange

                //Act
                sut.SubscribeDynamic<TestDynamicIntegrationEventHandler>("eventName");

                //Assert
                mockSubsManager.Verify(
                    _ => _.AddDynamicSubscription<TestDynamicIntegrationEventHandler>("eventName")
                );
            }
        }

        public class Subscribe
        {
            [Theory, AutoMoqData]
            public void Subscribe_HandlerNotRegistered_AddSubscription(
                [Frozen] Mock<IEventBusSubscriptionsManager> mockSubsManager,
                [Frozen] Mock<ServiceBusAdministrationClient> mockAdminClient,
                [Frozen] Mock<IServiceBusPersisterConnection> mockConnection,
                Mock<Response<SubscriptionProperties>> mockResponse,
                sb.EventBusServiceBus sut
            )
            {
                //Arrange
                mockAdminClient.Setup(_ => _.CreateSubscriptionAsync(
                    It.IsAny<CreateSubscriptionOptions>(),
                    It.IsAny<CreateRuleOptions>(),
                    It.IsAny<CancellationToken>()
                ))
                .Returns(Task.FromResult(mockResponse.Object));

                mockConnection.Setup(_ => _.AdminClient)
                    .Returns(mockAdminClient.Object);

                //Act
                sut.Subscribe<TestIntegrationEvent, TestIntegrationEventHandler>();

                //Assert
                mockAdminClient.Verify(_ => _.CreateSubscriptionAsync(
                    It.IsAny<CreateSubscriptionOptions>(),
                    It.IsAny<CreateRuleOptions>(),
                    It.IsAny<CancellationToken>()
                ));

                mockSubsManager.Verify(
                    _ => _.AddSubscription<TestIntegrationEvent, TestIntegrationEventHandler>()
                );
            }

            [Theory, AutoMoqData]
            public void Subscribe_HandlerRegistered_AddSubscription(
                [Frozen] Mock<IEventBusSubscriptionsManager> mockSubsManager,
                [Frozen] Mock<ServiceBusAdministrationClient> mockAdminClient,
                [Frozen] Mock<IServiceBusPersisterConnection> mockConnection,
                sb.EventBusServiceBus sut
            )
            {
                //Arrange
                mockConnection.Setup(_ => _.AdminClient)
                    .Returns(mockAdminClient.Object);

                mockSubsManager.Setup(_ => _.HasSubscriptionsForEvent<TestIntegrationEvent>())
                    .Returns(true);

                //Act
                sut.Subscribe<TestIntegrationEvent, TestIntegrationEventHandler>();

                //Assert
                mockAdminClient.Verify(
                    _ => _.CreateSubscriptionAsync(
                        It.IsAny<CreateSubscriptionOptions>(),
                        It.IsAny<CancellationToken>()
                    ),
                    Times.Never
                );

                mockSubsManager.Verify(
                    _ => _.AddSubscription<TestIntegrationEvent, TestIntegrationEventHandler>()
                );
            }

            [Theory, AutoMoqData]
            public void Subscribe_ServiceBusException_AddSubscriptionIsCalled(
                [Frozen] Mock<IEventBusSubscriptionsManager> mockSubsManager,
                [Frozen] Mock<ServiceBusAdministrationClient> mockAdminClient,
                [Frozen] Mock<IServiceBusPersisterConnection> mockConnection,
                sb.EventBusServiceBus sut
            )
            {
                //Arrange
                mockConnection.Setup(_ => _.AdminClient)
                    .Throws<Exception>();

                mockSubsManager.Setup(_ => _.HasSubscriptionsForEvent<TestIntegrationEvent>())
                    .Returns(false);

                //Act
                sut.Subscribe<TestIntegrationEvent, TestIntegrationEventHandler>();

                //Assert
                mockAdminClient.Verify(
                    _ => _.CreateSubscriptionAsync(
                        It.IsAny<CreateSubscriptionOptions>(),
                        It.IsAny<CancellationToken>()
                    ),
                    Times.Never
                );

                mockSubsManager.Verify(
                    _ => _.AddSubscription<TestIntegrationEvent, TestIntegrationEventHandler>()
                );
            }
        }

        public class UnsubscribeDynamic
        {
            [Theory, AutoMoqData]
            public void UnsubscribeDynamic_OK(
                [Frozen] Mock<IEventBusSubscriptionsManager> mockSubsManager,
                sb.EventBusServiceBus sut
            )
            {
                //Arrange

                //Act
                sut.UnsubscribeDynamic<TestDynamicIntegrationEventHandler>("eventName");

                //Assert
                mockSubsManager.Verify(
                    _ => _.RemoveDynamicSubscription<TestDynamicIntegrationEventHandler>("eventName")
                );
            }
        }

        public class Unsubscribe
        {
            [Theory, AutoMoqData]
            public void Unsubscribe_Ok_RemovedSubscription(
                [Frozen] Mock<IEventBusSubscriptionsManager> mockSubsManager,
                [Frozen] Mock<ServiceBusAdministrationClient> mockAdminClient,
                [Frozen] Mock<IServiceBusPersisterConnection> mockConnection,
                sb.EventBusServiceBus sut
            )
            {
                //Arrange
                mockConnection.Setup(_ => _.AdminClient)
                    .Returns(mockAdminClient.Object);

                //Act
                sut.Unsubscribe<TestIntegrationEvent, TestIntegrationEventHandler>();

                //Assert
                mockAdminClient.Verify(_ => _.DeleteSubscriptionAsync(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()
                ));

                mockSubsManager.Verify(
                    _ => _.RemoveSubscription<TestIntegrationEvent, TestIntegrationEventHandler>()
                );
            }

            [Theory, AutoMoqData]
            public void Unsubscribe_MessagingEntityNotFoundException_RemovedSubscription(
                [Frozen] Mock<IEventBusSubscriptionsManager> mockSubsManager,
                [Frozen] Mock<ServiceBusAdministrationClient> mockAdminClient,
                [Frozen] Mock<IServiceBusPersisterConnection> mockConnection,
                sb.EventBusServiceBus sut
            )
            {
                //Arrange
                mockConnection.Setup(_ => _.AdminClient)
                    .Throws<Exception>();

                //Act
                sut.Unsubscribe<TestIntegrationEvent, TestIntegrationEventHandler>();

                //Assert
                mockAdminClient.Verify(
                    _ => _.DeleteSubscriptionAsync(
                        It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<CancellationToken>()
                    ),
                    Times.Never
                );

                mockSubsManager.Verify(
                    _ => _.RemoveSubscription<TestIntegrationEvent, TestIntegrationEventHandler>()
                );
            }
        }

        public class Dispose
        {
            [Theory, AutoMoqData]
            public void Dispose_Ok(
                [Frozen] Mock<IEventBusSubscriptionsManager> mockSubsManager,
                sb.EventBusServiceBus sut
            )
            {
                //Arrange

                //Act
                sut.Dispose();

                //Assert
                mockSubsManager.Verify(
                    _ => _.Clear()
                );
            }
        }
    }
}