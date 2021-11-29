using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using System;
using System.Linq;
using Xunit;

namespace AW.SharedKernel.EventBus.UnitTests.EventBus
{
    public class InMemoryEventBusSubscriptionsManagerUnitTests
    {        
        public class IsEmpty
        {
            [Theory, AutoMoqData]
            public void IsEmpty_NoHandlersRegistered_ReturnsTrue(
                InMemoryEventBusSubscriptionsManager sut
            )
            {
                //Arrange

                //Act
                var result = sut.IsEmpty;

                //Assert
                result.Should().BeTrue();
            }

            [Theory, AutoMoqData]
            public void IsEmpty_HandlersRegistered_ReturnsFalse(
                InMemoryEventBusSubscriptionsManager sut
            )
            {
                //Arrange
                sut.AddSubscription<TestIntegrationEvent, TestIntegrationEventHandler>();

                //Act
                var result = sut.IsEmpty;

                //Assert
                result.Should().BeFalse();
            }
        }

        public class Clear
        {
            [Theory, AutoMoqData]
            public void Clear_NoHandlersRegistered_IsEmptyTrue(
                InMemoryEventBusSubscriptionsManager sut
            )
            {
                //Arrange

                //Act
                sut.Clear();

                //Assert
                sut.IsEmpty.Should().BeTrue();
            }

            [Theory, AutoMoqData]
            public void Clear_HandlersRegistered_IsEmptyTrue(
                InMemoryEventBusSubscriptionsManager sut
            )
            {
                //Arrange
                sut.AddSubscription<TestIntegrationEvent, TestIntegrationEventHandler>();

                //Act
                sut.Clear();

                //Assert
                sut.IsEmpty.Should().BeTrue();
            }
        }

        public class AddDynamicSubscription
        {
            [Theory, AutoMoqData]
            public void AddDynamicSubscription_NoHandlersRegistered_HandlerIsRegistered(
                InMemoryEventBusSubscriptionsManager sut
            )
            {
                //Arrange

                //Act
                sut.AddDynamicSubscription<TestDynamicIntegrationEventHandler>("eventName");

                //Assert
                sut.GetHandlersForEvent("eventName").ToList().Count.Should().Be(1);
            }

            [Theory, AutoMoqData]
            public void AddDynamicSubscription_HandlerTypeAlreadyRegistered_ThrowArgumentException(
                InMemoryEventBusSubscriptionsManager sut
            )
            {
                //Arrange
                sut.AddDynamicSubscription<TestDynamicIntegrationEventHandler>("eventName");

                //Act
                Action act = () => sut.AddDynamicSubscription<TestDynamicIntegrationEventHandler>("eventName");

                //Assert
                act.Should().Throw<ArgumentException>();
            }
        }

        public class AddSubscription
        {
            [Theory, AutoMoqData]
            public void AddSubscription_NoHandlersRegistered_HandlerIsRegistered(
                InMemoryEventBusSubscriptionsManager sut
            )
            {
                //Arrange

                //Act
                sut.AddSubscription<TestIntegrationEvent, TestIntegrationEventHandler>();

                //Assert
                sut.GetHandlersForEvent<TestIntegrationEvent>().ToList().Count.Should().Be(1);
            }

            [Theory, AutoMoqData]
            public void AddSubscription_HandlerAlreadyRegistered_ThrowArgumentException(
                InMemoryEventBusSubscriptionsManager sut
            )
            {
                //Arrange
                sut.AddSubscription<TestIntegrationEvent, TestIntegrationEventHandler>();

                //Act
                Action act = () => sut.AddSubscription<TestIntegrationEvent, TestIntegrationEventHandler>();

                //Assert
                act.Should().Throw<ArgumentException>();
            }
        }

        public class RemoveDynamicSubscription
        {
            [Theory, AutoMoqData]
            public void RemoveDynamicSubscription_HandlerNotRegistered_HandlerIsNotRemoved(
                InMemoryEventBusSubscriptionsManager sut
            )
            {
                //Arrange
                bool eventRemoved = false;
                sut.OnEventRemoved += new EventHandler<string>(delegate
                {
                    eventRemoved = true;
                });

                //Act
                sut.RemoveDynamicSubscription<TestDynamicIntegrationEventHandler>("eventName");

                //Assert
                eventRemoved.Should().BeFalse();
            }

            [Theory, AutoMoqData]
            public void RemoveDynamicSubscription_HandlerRegistered_HandlerIsRemoved(
                InMemoryEventBusSubscriptionsManager sut
            )
            {
                //Arrange
                bool eventRemoved = false;
                sut.OnEventRemoved += new EventHandler<string>(delegate
                {
                    eventRemoved = true;
                });
                sut.AddDynamicSubscription<TestDynamicIntegrationEventHandler>("eventName");

                //Act
                sut.RemoveDynamicSubscription<TestDynamicIntegrationEventHandler>("eventName");

                //Assert
                eventRemoved.Should().BeTrue();
            }
        }

        public class RemoveSubscription
        {
            [Theory, AutoMoqData]
            public void RemoveDynamicSubscription_HandlerNotRegistered_HandlerIsNotRemoved(
                InMemoryEventBusSubscriptionsManager sut
            )
            {
                //Arrange
                bool eventRemoved = false;
                sut.OnEventRemoved += new EventHandler<string>(delegate
                {
                    eventRemoved = true;
                });

                //Act
                sut.RemoveSubscription<TestIntegrationEvent, TestIntegrationEventHandler>();

                //Assert
                eventRemoved.Should().BeFalse();
            }

            [Theory, AutoMoqData]
            public void RemoveDynamicSubscription_HandlerRegistered_HandlerIsRemoved(
                InMemoryEventBusSubscriptionsManager sut
            )
            {
                //Arrange
                bool eventRemoved = false;
                sut.OnEventRemoved += new EventHandler<string>(delegate
                {
                    eventRemoved = true;
                });
                sut.AddSubscription<TestIntegrationEvent, TestIntegrationEventHandler>();

                //Act
                sut.RemoveSubscription<TestIntegrationEvent, TestIntegrationEventHandler>();

                //Assert
                eventRemoved.Should().BeTrue();
            }
        }
    }
}