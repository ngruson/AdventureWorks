using AW.SharedKernel.EventBus.AzureServiceBus;
using FluentAssertions;
using System.Threading.Tasks;
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
        public void AdminClient_ReturnAdminClient()
        {
            //Arrange
            var sut = new DefaultServiceBusPersisterConnection(connectionString, "my-subscription");

            //Act

            //Assert
            sut.AdminClient.Should().NotBeNull();
        }

        [Fact]
        public void Processor_ReturnProcessor()
        {
            //Arrange
            var sut = new DefaultServiceBusPersisterConnection(connectionString, "my-subscription");

            //Act

            //Assert
            sut.Processor.Should().NotBeNull();
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
        public async Task StartReceiving_ProcessorIsStarted()
        {
            //Arrange
            var sut = new DefaultServiceBusPersisterConnection(connectionString, "my-subscription");
            sut.Processor.ProcessMessageAsync += (args) => { return Task.CompletedTask; };
            sut.Processor.ProcessErrorAsync += (args) => { return Task.CompletedTask; };

            //Act
            await sut.StartReceiving();

            //Assert
            sut.Processor.IsProcessing.Should().Be(true);
        }

        [Fact]
        public async Task StopReceiving_ProcessorIsStopped()
        {
            //Arrange
            var sut = new DefaultServiceBusPersisterConnection(connectionString, "my-subscription");
            sut.Processor.ProcessMessageAsync += (args) => { return Task.CompletedTask; };
            sut.Processor.ProcessErrorAsync += (args) => { return Task.CompletedTask; };

            //Act
            await sut.StartReceiving();
            await sut.StopReceiving();

            //Assert
            sut.Processor.IsProcessing.Should().Be(false);
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