using AutoFixture.Xunit2;
using AW.Services.Basket.Core.Handlers.UpdateBasket;
using AW.Services.Basket.Core.Model;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.Basket.Core.UnitTests
{
    public class UpdateBasketCommandUnitTests
    {
        [Theory]
        [AutoMoqData]
        public async Task Handle_BasketExists_ReturnBasket(
            [Frozen] Mock<IBasketRepository> mockBasketRepository,
            UpdateBasketCommandHandler sut,
            UpdateBasketCommand command,
            CustomerBasket basket
        )
        {
            //Arrange
            mockBasketRepository.Setup(_ => _.UpdateBasketAsync(
                It.IsAny<CustomerBasket>()
            ))
            .ReturnsAsync(basket);

            //Act
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.Should().BeEquivalentTo(basket);
            mockBasketRepository.Verify(x => x.UpdateBasketAsync(
                It.IsAny<CustomerBasket>()
            ));
        }
    }
}