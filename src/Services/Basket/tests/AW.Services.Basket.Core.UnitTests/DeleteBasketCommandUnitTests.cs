using AutoFixture.Xunit2;
using AW.Services.Basket.Core.Handlers.DeleteBasket;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using MediatR;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.Basket.Core.UnitTests
{
    public class DeleteBasketCommandUnitTests
    {
        [Theory]
        [AutoMoqData]
        public async Task Handle_BasketDeleted(
            [Frozen] Mock<IBasketRepository> mockBasketRepository,
            DeleteBasketCommandHandler sut,
            DeleteBasketCommand command
        )
        {
            //Arrange

            //Act
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.Should().Be(Unit.Value);
            mockBasketRepository.Verify(_ => _.DeleteBasketAsync(
                It.IsAny<string>()
            ));
        }
    }
}