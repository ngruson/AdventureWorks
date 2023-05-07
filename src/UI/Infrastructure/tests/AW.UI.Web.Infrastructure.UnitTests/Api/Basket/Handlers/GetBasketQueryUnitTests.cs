using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Infrastructure.Api.Basket.Handlers.GetBasket;
using AW.UI.Web.Infrastructure.Api.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace AW.UI.Web.Infrastructure.UnitTests.Api.Basket.Handlers
{
    public class GetBasketQueryUnitTests
    {
        [Theory, AutoMoqData]
        public async Task Handle_WithCustomerNumber_CustomerReturned(
            [Frozen] Mock<IBasketApiClient> mockBasketApiClient,
            GetBasketQueryHandler sut,
            GetBasketQuery query,
            Infrastructure.Api.Basket.Handlers.GetBasket.Basket basket
        )
        {
            //Arrange
            mockBasketApiClient.Setup(_ => _.GetBasketAsync(
                    It.IsAny<string>()
                )
            )
            .ReturnsAsync(basket);

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.Should().Be(basket);

            mockBasketApiClient.Verify(x => x.GetBasketAsync(
                    It.IsAny<string>()
                )
            );
        }

        [Theory, AutoMoqData]
        public async Task Handle_WithoutUserId_ThrowsArgumentException(
            [Frozen] Mock<IBasketApiClient> mockBasketApiClient,
            GetBasketQueryHandler sut
        )
        {
            //Arrange
            var query = new GetBasketQuery(null);

            //Act
            Func<Task> func = async () => await sut.Handle(query, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<ArgumentException>()
                .WithMessage("Required input request.UserID was empty. (Parameter 'request.UserID')");

            mockBasketApiClient.Verify(x => x.GetBasketAsync(
                    It.IsAny<string>()
                ),
                Times.Never
            );
        }

        [Theory, AutoMoqData]
        public async Task Handle_ReturnedBasketNull_ThrowsArgumentNullException(
            [Frozen] Mock<IBasketApiClient> mockBasketApiClient,
            GetBasketQueryHandler sut,
            GetBasketQuery query
        )
        {
            //Arrange
            mockBasketApiClient.Setup(_ => _.GetBasketAsync(
                    It.IsAny<string>()
                )
            )
            .ReturnsAsync((Infrastructure.Api.Basket.Handlers.GetBasket.Basket?)null);

            //Act
            Func<Task> func = async () => await sut.Handle(query, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<ArgumentException>()
                .WithMessage("Value cannot be null. (Parameter 'basket')");

            mockBasketApiClient.Verify(x => x.GetBasketAsync(
                    It.IsAny<string>()
                )
            );
        }
    }
}
