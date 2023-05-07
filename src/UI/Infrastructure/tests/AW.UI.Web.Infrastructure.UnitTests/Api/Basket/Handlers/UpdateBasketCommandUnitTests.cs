using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Infrastructure.Api.Basket.Handlers.UpdateBasket;
using AW.UI.Web.Infrastructure.Api.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace AW.UI.Web.Infrastructure.UnitTests.Api.Basket.Handlers
{
    public class UpdateBasketCommandUnitTests
    {
        [Theory, AutoMoqData]
        public async Task Handle_WithBasket_ReturnUpdatedBasket(
            [Frozen] Mock<IBasketApiClient> mockBasketApiClient,
            UpdateBasketCommandHandler sut,
            UpdateBasketCommand command,
            Infrastructure.Api.Basket.Handlers.UpdateBasket.Basket basket
        )
        {
            //Arrange
            mockBasketApiClient.Setup(_ => _.UpdateBasketAsync(
                    It.IsAny<Infrastructure.Api.Basket.Handlers.UpdateBasket.Basket>()
                )
            )
            .ReturnsAsync(basket);

            //Act
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.Should().Be(basket);

            mockBasketApiClient.Verify(_ => _.UpdateBasketAsync(
                    It.IsAny<Infrastructure.Api.Basket.Handlers.UpdateBasket.Basket>()
                )
            );
        }

        [Theory, AutoMoqData]
        public async Task Handle_WithoutBasket_ThrowsArgumentException(
            [Frozen] Mock<IBasketApiClient> mockBasketApiClient,
            UpdateBasketCommandHandler sut
        )
        {
            //Arrange
            var query = new UpdateBasketCommand(null);

            //Act
            Func<Task> func = async () => await sut.Handle(query, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<ArgumentException>()
                .WithMessage("Value cannot be null. (Parameter 'request.Basket')");

            mockBasketApiClient.Verify(_ => _.UpdateBasketAsync(
                    It.IsAny<Infrastructure.Api.Basket.Handlers.UpdateBasket.Basket>()
                ),
                Times.Never
            );
        }

        [Theory, AutoMoqData]
        public async Task Handle_ReturnedBasketNull_ThrowsArgumentNullException(
            [Frozen] Mock<IBasketApiClient> mockBasketApiClient,
            UpdateBasketCommandHandler sut,
            UpdateBasketCommand command
        )
        {
            //Arrange
            mockBasketApiClient.Setup(_ => _.UpdateBasketAsync(command.Basket))
                .ReturnsAsync((Infrastructure.Api.Basket.Handlers.UpdateBasket.Basket?)null);

            //Act
            Func<Task> func = async () => await sut.Handle(command, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<ArgumentException>()
                .WithMessage("Value cannot be null. (Parameter 'basket')");

            mockBasketApiClient.Verify(_ => _.UpdateBasketAsync(
                    It.IsAny<Infrastructure.Api.Basket.Handlers.UpdateBasket.Basket>()
                )
            );
        }
    }
}
