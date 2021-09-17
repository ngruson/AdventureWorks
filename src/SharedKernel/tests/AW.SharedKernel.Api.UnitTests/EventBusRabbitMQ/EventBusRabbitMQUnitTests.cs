using AutoFixture.Xunit2;
using AW.SharedKernel.Api.EventBus;
using AW.SharedKernel.Api.EventBus.Events;
using AW.SharedKernel.Api.EventBusRabbitMQ;
using AW.SharedKernel.UnitTesting;
using Moq;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using Xunit;
using mq = AW.SharedKernel.Api.EventBusRabbitMQ;

namespace AW.SharedKernel.Api.UnitTests.EventBusRabbitMQ
{
    public class EventBusRabbitMQUnitTests
    {
        [Theory, AutoMoqData]
        public void Publish(
            [Frozen] Mock<IRabbitMQPersistentConnection> mockConnection,
            [Frozen] Mock<IModel> mockChannel,
            [Greedy] mq.EventBusRabbitMQ sut,
            IntegrationEvent integrationEvent
        )
        {
            //Arrange            
            mockConnection.Setup(_ => _.CreateModel())
                .Returns(mockChannel.Object);

            //Act
            sut.Publish(integrationEvent);

            //Assert
            mockChannel.Verify(_ => _.BasicPublish(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<bool>(),
                It.IsAny<IBasicProperties>(),
                It.IsAny<ReadOnlyMemory<byte>>()
            ));
        }

        [Theory, AutoMoqData]
        public void SubscribeDynamic(
            [Frozen] Mock<IRabbitMQPersistentConnection> mockConnection,
            [Frozen] Mock<IModel> mockChannel,
            [Greedy] mq.EventBusRabbitMQ sut,
            Mock<QueueDeclareOk> mockQueueDeclareOk
        )
        {
            //Arrange            
            mockChannel.Setup(_ => _.QueueDeclare(
                It.IsAny<string>(),
                It.IsAny<bool>(),
                It.IsAny<bool>(),
                It.IsAny<bool>(),
                It.IsAny<IDictionary<string, object>>()
            ))
            .Returns(mockQueueDeclareOk.Object);

            mockConnection.Setup(_ => _.CreateModel())
                .Returns(mockChannel.Object);

            //Act
            sut.SubscribeDynamic<TestDynamicIntegrationEventHandler>("eventName");

            //Assert
            mockChannel.Verify(_ => _.QueueBind(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<IDictionary<string, object>>()
            ));
        }

        [Theory, AutoMoqData]
        public void SubscribeDynamic_SecondTime(
            [Frozen] Mock<IRabbitMQPersistentConnection> mockConnection,
            [Frozen] Mock<IModel> mockChannel,
            [Frozen] Mock<IEventBusSubscriptionsManager> mockSubsManager,
            [Greedy] mq.EventBusRabbitMQ sut,
            Mock<QueueDeclareOk> mockQueueDeclareOk
        )
        {
            //Arrange            
            mockChannel.Setup(_ => _.QueueDeclare(
                It.IsAny<string>(),
                It.IsAny<bool>(),
                It.IsAny<bool>(),
                It.IsAny<bool>(),
                It.IsAny<IDictionary<string, object>>()
            ))
            .Returns(mockQueueDeclareOk.Object);

            mockConnection.Setup(_ => _.CreateModel())
                .Returns(mockChannel.Object);

            mockSubsManager.SetupSequence(_ => _.HasSubscriptionsForEvent(
                It.IsAny<string>())
            )
            .Returns(false)
            .Returns(true);

            //Act
            sut.SubscribeDynamic<TestDynamicIntegrationEventHandler>("eventName");
            sut.SubscribeDynamic<TestDynamicIntegrationEventHandler>("eventName");

            //Assert
            mockChannel.Verify(_ => _.QueueBind(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<IDictionary<string, object>>()
            ), Times.Once);
        }

        [Theory, AutoMoqData]
        public void Subscribe(
            [Frozen] Mock<IRabbitMQPersistentConnection> mockConnection,
            [Frozen] Mock<IModel> mockChannel,
            [Greedy] mq.EventBusRabbitMQ sut,
            Mock<QueueDeclareOk> mockQueueDeclareOk
        )
        {
            //Arrange            
            mockChannel.Setup(_ => _.QueueDeclare(
                It.IsAny<string>(),
                It.IsAny<bool>(),
                It.IsAny<bool>(),
                It.IsAny<bool>(),
                It.IsAny<IDictionary<string, object>>()
            ))
            .Returns(mockQueueDeclareOk.Object);

            mockConnection.Setup(_ => _.CreateModel())
                .Returns(mockChannel.Object);

            //Act
            sut.Subscribe<TestIntegrationEvent, TestIntegrationEventHandler>();

            //Assert
            mockChannel.Verify(_ => _.QueueBind(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<IDictionary<string, object>>()
            ));
        }

        [Theory, AutoMoqData]
        public void Subscribe_SecondTime(
            [Frozen] Mock<IRabbitMQPersistentConnection> mockConnection,
            [Frozen] Mock<IModel> mockChannel,
            [Frozen] Mock<IEventBusSubscriptionsManager> mockSubsManager,
            [Greedy] mq.EventBusRabbitMQ sut,
            Mock<QueueDeclareOk> mockQueueDeclareOk
        )
        {
            //Arrange            
            mockChannel.Setup(_ => _.QueueDeclare(
                It.IsAny<string>(),
                It.IsAny<bool>(),
                It.IsAny<bool>(),
                It.IsAny<bool>(),
                It.IsAny<IDictionary<string, object>>()
            ))
            .Returns(mockQueueDeclareOk.Object);

            mockConnection.Setup(_ => _.CreateModel())
                .Returns(mockChannel.Object);

            mockSubsManager.SetupSequence(_ => _.HasSubscriptionsForEvent(
                It.IsAny<string>())
            )
            .Returns(false)
            .Returns(true);

            //Act
            sut.Subscribe<TestIntegrationEvent, TestIntegrationEventHandler>();
            sut.Subscribe<TestIntegrationEvent, TestIntegrationEventHandler>();

            //Assert
            mockChannel.Verify(_ => _.QueueBind(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<IDictionary<string, object>>()
            ), Times.Once);
        }

        [Theory, AutoMoqData]
        public void UnsubscribeDynamic(
            [Frozen] Mock<IEventBusSubscriptionsManager> mockSubsManager,
            [Greedy] mq.EventBusRabbitMQ sut
        )
        {
            //Arrange

            //Act
            sut.UnsubscribeDynamic<TestDynamicIntegrationEventHandler>("eventName");

            //Assert
            mockSubsManager.Verify(
                _ => _.RemoveDynamicSubscription<TestDynamicIntegrationEventHandler>(
                    It.IsAny<string>()
                )
            );
        }

        [Theory, AutoMoqData]
        public void Unsubscribe(
            [Frozen] Mock<IEventBusSubscriptionsManager> mockSubsManager,
            [Greedy] mq.EventBusRabbitMQ sut
        )
        {
            //Arrange

            //Act
            sut.Unsubscribe<TestIntegrationEvent, TestIntegrationEventHandler>();

            //Assert
            mockSubsManager.Verify(
                _ => _.GetEventKey<TestIntegrationEvent>()
            );

            mockSubsManager.Verify(
                _ => _.RemoveSubscription<TestIntegrationEvent, TestIntegrationEventHandler>()
            );
        }

        [Theory, AutoMoqData]
        public void Dispose_ConsumerChannelIsNotNull_DisposeConsumerChannel(
            [Frozen] Mock<IRabbitMQPersistentConnection> mockConnection,
            [Frozen] Mock<IModel> mockChannel,
            [Frozen] Mock<IEventBusSubscriptionsManager> mockSubsManager,
            [Greedy] mq.EventBusRabbitMQ sut,
            Mock<QueueDeclareOk> mockQueueDeclareOk
        )
        {
            //Arrange
            mockChannel.Setup(_ => _.QueueDeclare(
               It.IsAny<string>(),
               It.IsAny<bool>(),
               It.IsAny<bool>(),
               It.IsAny<bool>(),
               It.IsAny<IDictionary<string, object>>()
           ))
           .Returns(mockQueueDeclareOk.Object);

            mockConnection.Setup(_ => _.CreateModel())
                .Returns(mockChannel.Object);

            //Act
            sut.Subscribe<TestIntegrationEvent, TestIntegrationEventHandler>();
            sut.Dispose();

            //Assert
            mockChannel.Verify(_ => _.Dispose());

            mockSubsManager.Verify(
                _ => _.Clear()
            );
        }

        [Theory, AutoMoqData]
        public void Dispose_ConsumerChannelIsNull_ClearSubscription(
            [Frozen] Mock<IEventBusSubscriptionsManager> mockSubsManager,
            [Greedy] mq.EventBusRabbitMQ sut
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