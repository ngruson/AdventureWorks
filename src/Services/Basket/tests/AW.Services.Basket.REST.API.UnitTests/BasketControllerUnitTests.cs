using AutoFixture.Xunit2;
using AW.Services.Basket.Core.Handlers.Checkout;
using AW.Services.Basket.Core.Handlers.DeleteBasket;
using AW.Services.Basket.Core.Handlers.GetBasket;
using AW.Services.Basket.Core.Handlers.UpdateBasket;
using AW.Services.Basket.Core.Model;
using AW.Services.Basket.REST.API.Controllers;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.Basket.REST.API.UnitTests
{
    public class BasketControllerUnitTests
    {
        public class GetBasketById
        {
            [Theory, AutoMoqData]
            public async Task GetBasketById_BasketExists_ReturnBasket(
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] BasketController sut,
            string basketId,
            CustomerBasket basket
        )
            {
                //Arrange
                mockMediator.Setup(x => x.Send(It.IsAny<GetBasketQuery>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(basket);

                //Act
                var actionResult = await sut.GetBasketByIdAsync(basketId);

                //Assert
                var okObjectResult = actionResult as OkObjectResult;
                okObjectResult.Should().NotBeNull();

                var resultBasket = okObjectResult.Value.Should().BeAssignableTo<CustomerBasket>().Subject;
                resultBasket.Should().Be(basket);
            }

            [Theory, AutoMoqData]
            public async Task GetBasketById_BasketDoesNotExist_ReturnNewBasket(
                [Frozen] Mock<IMediator> mockMediator,
                [Greedy] BasketController sut,
                string basketId
            )
            {
                //Arrange
                mockMediator.Setup(x => x.Send(It.IsAny<GetBasketQuery>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync((CustomerBasket)null);

                //Act
                var actionResult = await sut.GetBasketByIdAsync(basketId);

                //Assert
                var okObjectResult = actionResult as OkObjectResult;
                okObjectResult.Should().NotBeNull();

                var resultBasket = okObjectResult.Value.Should().BeAssignableTo<CustomerBasket>().Subject;
                resultBasket.BuyerId.Should().Be(basketId);
            }
        }
 
        public class UpdateBasket
        {
            [Theory, AutoMoqData]
            public async Task UpdateBasket_BasketExists_ReturnBasket(
                [Frozen] Mock<IMediator> mockMediator,
                [Greedy] BasketController sut,
                CustomerBasket basket
            )
            {
                //Arrange
                mockMediator.Setup(x => x.Send(It.IsAny<UpdateBasketCommand>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(basket);

                //Act
                var actionResult = await sut.UpdateBasketAsync(basket);

                //Assert
                var okObjectResult = actionResult as OkObjectResult;
                okObjectResult.Should().NotBeNull();

                var resultBasket = okObjectResult.Value.Should().BeAssignableTo<CustomerBasket>().Subject;
                resultBasket.Should().Be(basket);
            }
        }

        public class Checkout
        {
            [Theory, AutoMoqData]
            public async Task Checkout_BasketExists_ReturnAcceptedResult(
                [Frozen] Mock<IMediator> mockMediator,
                [Greedy] BasketController sut,
                CustomerBasket basket,
                BasketCheckout checkout,
                string requestId
            )
            {
                //Arrange
                mockMediator.Setup(x => x.Send(It.IsAny<CheckoutCommand>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(basket);

                //Act
                var actionResult = await sut.CheckoutAsync(checkout, requestId);

                //Assert
                var okObjectResult = actionResult as AcceptedResult;
                okObjectResult.Should().NotBeNull();
            }

            [Theory, AutoMoqData]
            public async Task Checkout_BasketDoesNotExist_ReturnBadRequestResult(
                [Frozen] Mock<IMediator> mockMediator,
                [Greedy] BasketController sut,
                BasketCheckout checkout,
                string requestId
            )
            {
                //Arrange
                mockMediator.Setup(x => x.Send(It.IsAny<CheckoutCommand>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync((CustomerBasket)null);

                //Act
                var actionResult = await sut.CheckoutAsync(checkout, requestId);

                //Assert
                var okObjectResult = actionResult as BadRequestResult;
                okObjectResult.Should().NotBeNull();
            }
        }

        public class DeleteBasket
        {
            [Theory, AutoMoqData]
            public async Task DeleteBasket_BasketExists_ReturnAcceptedResult(
                [Frozen] Mock<IMediator> mockMediator,
                [Greedy] BasketController sut,
                string basketId
            )
            {
                //Arrange

                //Act
                await sut.DeleteBasketByIdAsync(basketId);

                //Assert
                mockMediator.Verify(_ => _.Send(
                    It.IsAny<DeleteBasketCommand>(),
                    It.IsAny<CancellationToken>()
                ));
            }
        }
    }
}