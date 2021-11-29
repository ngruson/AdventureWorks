using AW.SharedKernel.EventBus.AzureServiceBus;
using FluentAssertions;
using Xunit;

namespace AW.SharedKernel.Api.UnitTests.EventBusServiceBus
{
    public class DefaultServiceBusPersisterConnectionUnitTests
    {
        private readonly string connectionString = "Endpoint=sb://namespacename.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=yZOy10HAJTNkyaKOqwjSricOEud7QLK7R62KyVfjCt4=";

        [Fact]
        public void ServiceBusClient_ReturnServiceBusClient()
        {
            //Arrange
            var sut = new DefaultServiceBusPersisterConnection(connectionString, "my-subscription");

            //Act

            //Assert
            sut.ServiceBusClient.Should().NotBeNull();
        }

        [Fact]
        public void CreateModel_ReturnServiceBusClient()
        {
            //Arrange
            var sut = new DefaultServiceBusPersisterConnection(connectionString, "my-subscription");

            //Act
            var result = sut.CreateModel();

            //Assert
            result.Should().Be(sut.ServiceBusClient);
        }

        [Fact]
        public void Dispose()
        {
            //Arrange
            var sut = new DefaultServiceBusPersisterConnection(connectionString, "my-subscription");

            //Act
            sut.Dispose();

            //Assert
            true.Should().Be(true);
        }
    }
}