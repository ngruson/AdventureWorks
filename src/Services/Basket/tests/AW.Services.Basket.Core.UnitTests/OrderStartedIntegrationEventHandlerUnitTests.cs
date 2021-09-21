using AutoFixture.Xunit2;
using AW.Services.Basket.Core.IntegrationEvents.EventHandling;
using AW.Services.Basket.Core.IntegrationEvents.Events;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading;
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

        [Theory]
        [AutoMoqData]
        public void Handle_RepositoryIsNull_ThrowArgumentNullException(
            [Frozen] Mock<ILogger<OrderStartedIntegrationEventHandler>> mockLogger
        )
        {
            //Arrange

            //Act
            Action act = () =>
            {
                OrderStartedIntegrationEventHandler sut = new(
                    null, mockLogger.Object
                );
            };

            //Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [AutoMoqData]
        public void Handle_LoggerIsNull_ThrowArgumentNullException(
            [Frozen] Mock<IBasketRepository> mockBasketRepository
        )
        {
            //Arrange

            //Act
            Action act = () =>
            {
                OrderStartedIntegrationEventHandler sut = new(
                    mockBasketRepository.Object, null
                );
            };

            //Assert
            act.Should().Throw<ArgumentNullException>();
        }
    }
}