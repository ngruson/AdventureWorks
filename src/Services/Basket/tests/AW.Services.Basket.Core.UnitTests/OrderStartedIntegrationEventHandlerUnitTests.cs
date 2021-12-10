using AutoFixture.Xunit2;
using AW.Services.Basket.Core.IntegrationEvents.EventHandling;
using AW.Services.Basket.Core.IntegrationEvents.Events;
using AW.SharedKernel.UnitTesting;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.Basket.Core.UnitTests
{
    public class OrderStartedIntegrationEventHandlerUnitTests
    {
        [Theory]
        [AutoMoqData]
        public async Task Handle_DeleteBasket(
            [Frozen] Mock<IBasketRepository> mockBasketRepository,
            OrderStartedIntegrationEventHandler sut,
            OrderStartedIntegrationEvent orderStartedEvent
        )
        {
            //Arrange

            //Act
            await sut.Handle(orderStartedEvent);

            //Assert
            mockBasketRepository.Verify(_ => _.DeleteBasketAsync(
                It.IsAny<string>()
            ));
        }
    }
}