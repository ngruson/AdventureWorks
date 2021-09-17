using AutoFixture.Xunit2;
using AW.SharedKernel.Api.EventBus;
using AW.SharedKernel.Api.EventBus.Events;
using AW.SharedKernel.Api.EventBusServiceBus;
using AW.SharedKernel.UnitTesting;
using Microsoft.Azure.ServiceBus;
using Moq;
using Xunit;
using sb = AW.SharedKernel.Api.EventBusServiceBus;

namespace AW.SharedKernel.Api.UnitTests.EventBusServiceBus
{
    public class EventBusServiceBusUnitTests
    {
        public class Publish
        {
            [Theory, AutoMoqData]
            public void Publish_OK(
                [Frozen] Mock<IServiceBusPersisterConnection> mockConnection,
                [Frozen] Mock<ITopicClient> mockTopicClient,
                sb.EventBusServiceBus sut,
                TestIntegrationEvent integrationEvent
            )
            {
                //Arrange            
                mockConnection.Setup(_ => _.TopicClient)
                    .Returns(mockTopicClient.Object);

                //Act
                sut.Publish(integrationEvent);

                //Assert
                mockTopicClient.Verify(_ => _.SendAsync(It.IsAny<Message>()));
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
                [Frozen] Mock<ISubscriptionClient> mockSubscriptionClient,
                [Frozen] Mock<IServiceBusPersisterConnection> mockConnection,
                sb.EventBusServiceBus sut
            )
            {
                //Arrange
                mockConnection.Setup(_ => _.SubscriptionClient)
                    .Returns(mockSubscriptionClient.Object);

                //Act
                sut.Subscribe<TestIntegrationEvent, TestIntegrationEventHandler>();

                //Assert
                mockSubscriptionClient.Verify(_ => _.AddRuleAsync(It.IsAny<RuleDescription>()));

                mockSubsManager.Verify(
                    _ => _.AddSubscription<TestIntegrationEvent, TestIntegrationEventHandler>()
                );
            }

            [Theory, AutoMoqData]
            public void Subscribe_HandlerRegistered_AddSubscription(
                [Frozen] Mock<IEventBusSubscriptionsManager> mockSubsManager,
                [Frozen] Mock<ISubscriptionClient> mockSubscriptionClient,
                [Frozen] Mock<IServiceBusPersisterConnection> mockConnection,
                sb.EventBusServiceBus sut
            )
            {
                //Arrange
                mockConnection.Setup(_ => _.SubscriptionClient)
                    .Returns(mockSubscriptionClient.Object);

                mockSubsManager.Setup(_ => _.HasSubscriptionsForEvent<TestIntegrationEvent>())
                    .Returns(true);

                //Act
                sut.Subscribe<TestIntegrationEvent, TestIntegrationEventHandler>();

                //Assert
                mockSubscriptionClient.Verify(
                    _ => _.AddRuleAsync(It.IsAny<RuleDescription>()),
                    Times.Never
                );

                mockSubsManager.Verify(
                    _ => _.AddSubscription<TestIntegrationEvent, TestIntegrationEventHandler>()
                );
            }

            [Theory, AutoMoqData]
            public void Subscribe_ServiceBusException_AddSubscription(
                [Frozen] Mock<IEventBusSubscriptionsManager> mockSubsManager,
                [Frozen] Mock<ISubscriptionClient> mockSubscriptionClient,
                [Frozen] Mock<IServiceBusPersisterConnection> mockConnection,
                sb.EventBusServiceBus sut
            )
            {
                //Arrange
                mockConnection.Setup(_ => _.SubscriptionClient)
                    .Throws(new ServiceBusException(true));

                mockSubsManager.Setup(_ => _.HasSubscriptionsForEvent<TestIntegrationEvent>())
                    .Returns(true);

                //Act
                sut.Subscribe<TestIntegrationEvent, TestIntegrationEventHandler>();

                //Assert
                mockSubscriptionClient.Verify(
                    _ => _.AddRuleAsync(It.IsAny<RuleDescription>()),
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
                [Frozen] Mock<ISubscriptionClient> mockSubscriptionClient,
                [Frozen] Mock<IServiceBusPersisterConnection> mockConnection,
                sb.EventBusServiceBus sut
            )
            {
                //Arrange
                mockConnection.Setup(_ => _.SubscriptionClient)
                    .Returns(mockSubscriptionClient.Object);

                //Act
                sut.Unsubscribe<TestIntegrationEvent, TestIntegrationEventHandler>();

                //Assert
                mockSubscriptionClient.Verify(_ => _.RemoveRuleAsync(It.IsAny<string>()));

                mockSubsManager.Verify(
                    _ => _.RemoveSubscription<TestIntegrationEvent, TestIntegrationEventHandler>()
                );
            }

            [Theory, AutoMoqData]
            public void Unsubscribe_MessagingEntityNotFoundException_RemovedSubscription(
                [Frozen] Mock<IEventBusSubscriptionsManager> mockSubsManager,
                [Frozen] Mock<ISubscriptionClient> mockSubscriptionClient,
                [Frozen] Mock<IServiceBusPersisterConnection> mockConnection,
                sb.EventBusServiceBus sut
            )
            {
                //Arrange
                mockConnection.Setup(_ => _.SubscriptionClient)
                    .Throws(new MessagingEntityNotFoundException("message"));

                //Act
                sut.Unsubscribe<TestIntegrationEvent, TestIntegrationEventHandler>();

                //Assert
                mockSubscriptionClient.Verify(
                    _ => _.RemoveRuleAsync(It.IsAny<string>()),
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