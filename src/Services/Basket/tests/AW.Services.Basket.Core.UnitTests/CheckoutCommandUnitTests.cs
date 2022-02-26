using AutoFixture.Xunit2;
using AW.Services.Basket.Core.Handlers.Checkout;
using AW.Services.Basket.Core.Models;
using AW.Services.Infrastructure.EventBus.Abstractions;
using AW.Services.Infrastructure.EventBus.Events;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.Basket.Core.UnitTests
{
    public class CheckoutCommandUnitTests
    {
        [Theory]
        [AutoMoqData]
        public async Task Handle_BasketExists_ReturnBasket(
            [Frozen] Mock<IBasketRepository> mockBasketRepository,
            [Frozen] Mock<IEventBus> mockEventBus,
            CheckoutCommandHandler sut,
            CheckoutCommand command,
            CustomerBasket basket
        )
        {
            //Arrange
            mockBasketRepository.Setup(_ => _.GetBasketAsync(
                It.IsAny<string>()
            ))
            .ReturnsAsync(basket);

            //Act
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.Should().BeEquivalentTo(basket);
            mockBasketRepository.Verify(_ => _.GetBasketAsync(
                It.IsAny<string>()
            ));
            mockEventBus.Verify(_ => _.Publish(It.IsAny<IntegrationEvent>()));
        }

        [Theory]
        [AutoMoqData]
        public async Task Handle_BasketDoesNotExist_ReturnNull(
            [Frozen] Mock<IBasketRepository> mockBasketRepository,
            [Frozen] Mock<IEventBus> mockEventBus,
            CheckoutCommandHandler sut,
            CheckoutCommand command
        )
        {
            //Arrange

            //Act
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.Should().BeNull();
            mockBasketRepository.Verify(x => x.GetBasketAsync(
                It.IsAny<string>()
            ));
            mockEventBus.Verify(_ => _.Publish(It.IsAny<IntegrationEvent>()), Times.Never);
        }

        [Theory]
        [AutoMoqData]
        public async Task Handle_EventBusThrowsException_ThrowException(
            [Frozen] Mock<IBasketRepository> mockBasketRepository,
            [Frozen] Mock<IEventBus> mockEventBus,
            CheckoutCommandHandler sut,
            CheckoutCommand command,
            CustomerBasket basket
        )
        {
            //Arrange
            mockBasketRepository.Setup(_ => _.GetBasketAsync(
                It.IsAny<string>()
            ))
            .ReturnsAsync(basket);

            mockEventBus.Setup(_ => _.Publish(It.IsAny<IntegrationEvent>()))
                .Throws<Exception>();

            //Act
            Func<Task> func = async () => await sut.Handle(command, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<Exception>();
            mockBasketRepository.Verify(x => x.GetBasketAsync(
                It.IsAny<string>()
            ));
        }
    }
}