using AutoFixture.Xunit2;
using AW.UI.Web.SharedKernel.Basket.Handlers.GetBasket;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using FluentAssertions;
using Moq;

namespace AW.UI.Web.SharedKernel.UnitTests.Basket.Handlers
{
    public class GetBasketQueryUnitTests
    {
        [Theory, AutoMoqData]
        public async Task Handle_WithCustomerNumber_CustomerReturned(
            [Frozen] Mock<IBasketApiClient> mockBasketApiClient,
            GetBasketQueryHandler sut,
            GetBasketQuery query,
            SharedKernel.Basket.Handlers.GetBasket.Basket basket
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