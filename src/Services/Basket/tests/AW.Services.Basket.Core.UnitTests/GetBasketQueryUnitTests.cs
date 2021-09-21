using AutoFixture.Xunit2;
using AW.Services.Basket.Core.Handlers.GetBasket;
using AW.Services.Basket.Core.Model;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.Basket.Core.UnitTests
{
    public class GetBasketQueryUnitTests
    {
        [Theory]
        [AutoMoqData]
        public async Task Handle_BasketExists_ReturnBasket(
            [Frozen] Mock<IBasketRepository> mockBasketRepository,
            GetBasketQueryHandler sut,
            GetBasketQuery query,
            CustomerBasket basket
        )
        {
            //Arrange
            mockBasketRepository.Setup(_ => _.GetBasketAsync(
                It.IsAny<string>()
            ))
            .ReturnsAsync(basket);

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.Should().BeEquivalentTo(basket);
            mockBasketRepository.Verify(x => x.GetBasketAsync(
                It.IsAny<string>()
            ));
        }
    }
}