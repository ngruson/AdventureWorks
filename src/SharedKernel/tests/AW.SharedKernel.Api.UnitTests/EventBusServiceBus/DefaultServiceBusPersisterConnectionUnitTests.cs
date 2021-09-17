using AW.SharedKernel.Api.EventBusServiceBus;
using FluentAssertions;
using Microsoft.Azure.ServiceBus;
using Xunit;

namespace AW.SharedKernel.Api.UnitTests.EventBusServiceBus
{
    public class DefaultServiceBusPersisterConnectionUnitTests
    {
        private readonly ServiceBusConnectionStringBuilder sb = new ServiceBusConnectionStringBuilder
        {
            Endpoint = "test.servicebus.windows.net",
            EntityPath = "my-topic",
            SasKeyName = "RootManageSharedAccessKey",
            SasKey = "test"
        };

        [Fact]
        public void TopicClient_ReturnTopicClient()
        {
            //Arrange
            var sut = new DefaultServiceBusPersisterConnection(sb, "my-subscription");

            //Act
            var result = sut.TopicClient;

            //Assert
            result.TopicName.Should().Be("my-topic");
        }

        [Fact]
        public void SubscriptionClient_ReturnSubscriptionClient()
        {
            //Arrange
            var sut = new DefaultServiceBusPersisterConnection(sb, "my-subscription");

            //Act
            var result = sut.SubscriptionClient;

            //Assert
            result.SubscriptionName.Should().Be("my-subscription");
            result.TopicPath.Should().Be("my-topic");
        }

        [Fact]
        public void CreateModel_ReturnTopicClient()
        {
            //Arrange
            var sut = new DefaultServiceBusPersisterConnection(sb, "my-subscription");

            //Act
            var result = sut.CreateModel();

            //Assert
            result.TopicName.Should().Be("my-topic");
        }

        [Fact]
        public void Dispose()
        {
            //Arrange
            var sut = new DefaultServiceBusPersisterConnection(sb, "my-subscription");

            //Act
            sut.Dispose();

            //Assert
            true.Should().Be(true);
        }
    }
}