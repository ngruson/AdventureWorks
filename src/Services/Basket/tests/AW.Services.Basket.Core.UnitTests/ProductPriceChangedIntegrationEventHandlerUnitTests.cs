using AutoFixture.Xunit2;
using AW.Services.Basket.Core.IntegrationEvents.EventHandling;
using AW.Services.Basket.Core.IntegrationEvents.Events;
using AW.Services.Basket.Core.Models;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Moq;
using Xunit;

namespace AW.Services.Basket.Core.UnitTests
{
    public class ProductPriceChangedIntegrationEventHandlerUnitTests
    {
        [Theory]
        [AutoMoqData]
        public async Task Handle_BasketHasProductWithOldPrice_UpdateUnitPrice(
            [Frozen] Mock<IBasketRepository> mockBasketRepository,
            ProductPriceChangedIntegrationEventHandler sut,
            ProductPriceChangedIntegrationEvent productPriceChangedEvent,
            string user1,
            string user2
        )
        {
            //Arrange
            mockBasketRepository.Setup(_ => _.GetUsers())
                .Returns(new string[] { user1, user2 });

            var basket_user1 = new CustomerBasket
            {
                Items = new List<BasketItem>
                    {
                        new BasketItem
                        {
                            ProductNumber = productPriceChangedEvent.ProductNumber,
                            UnitPrice = productPriceChangedEvent.OldPrice
                        }
                    }
            };

            var basket_user2 = new CustomerBasket
            {
                Items = new List<BasketItem>
                    {
                        new BasketItem
                        {
                            ProductNumber = productPriceChangedEvent.ProductNumber,
                            UnitPrice = productPriceChangedEvent.OldPrice
                        }
                    }
            };

            mockBasketRepository.Setup(_ => _.GetBasketAsync(It.Is<string>(_ => _ == user1)))
                .ReturnsAsync(basket_user1);

            mockBasketRepository.Setup(_ => _.GetBasketAsync(It.Is<string>(_ => _ == user2)))
                .ReturnsAsync(basket_user2);

            //Act
            await sut.Handle(productPriceChangedEvent);

            //Assert
            mockBasketRepository.Verify(_ => _.UpdateBasketAsync(
                It.Is<CustomerBasket>(_ => _ == basket_user1)
            ));

            mockBasketRepository.Verify(_ => _.UpdateBasketAsync(
                It.Is<CustomerBasket>(_ => _ == basket_user2)
            ));

            basket_user1.Items[0].UnitPrice.Should().Be(productPriceChangedEvent.NewPrice);
            basket_user1.Items[0].OldUnitPrice.Should().Be(productPriceChangedEvent.OldPrice);

            basket_user2.Items[0].UnitPrice.Should().Be(productPriceChangedEvent.NewPrice);
            basket_user2.Items[0].OldUnitPrice.Should().Be(productPriceChangedEvent.OldPrice);
        }

        [Theory]
        [AutoMoqData]
        public async Task Handle_BasketHasProductWithoutOldPrice_UnitPriceHasNotChanged(
            [Frozen] Mock<IBasketRepository> mockBasketRepository,
            ProductPriceChangedIntegrationEventHandler sut,
            ProductPriceChangedIntegrationEvent productPriceChangedEvent,
            string user1
        )
        {
            //Arrange
            mockBasketRepository.Setup(_ => _.GetUsers())
                .Returns(new string[] { user1 });

            var basket_user1 = new CustomerBasket
            {
                Items = new List<BasketItem>
                    {
                        new BasketItem
                        {
                            ProductNumber = productPriceChangedEvent.ProductNumber,
                            UnitPrice = productPriceChangedEvent.OldPrice - 1
                        }
                    }
            };

            mockBasketRepository.Setup(_ => _.GetBasketAsync(It.Is<string>(_ => _ == user1)))
                .ReturnsAsync(basket_user1);

            //Act
            await sut.Handle(productPriceChangedEvent);

            //Assert
            mockBasketRepository.Verify(_ => _.UpdateBasketAsync(
                It.Is<CustomerBasket>(_ => _ == basket_user1)
            ));

            basket_user1.Items[0].UnitPrice.Should().Be(productPriceChangedEvent.OldPrice - 1);
            basket_user1.Items[0].OldUnitPrice.Should().Be(0);
        }

        [Theory]
        [AutoMoqData]
        public async Task Handle_BasketDoesNotHaveProduct_BasketNotUpdated(
            [Frozen] Mock<IBasketRepository> mockBasketRepository,
            ProductPriceChangedIntegrationEventHandler sut,
            ProductPriceChangedIntegrationEvent productPriceChangedEvent,
            string user1,
            CustomerBasket basket
        )
        {
            //Arrange
            mockBasketRepository.Setup(_ => _.GetUsers())
                .Returns(new string[] { user1 });

            mockBasketRepository.Setup(_ => _.GetBasketAsync(It.Is<string>(_ => _ == user1)))
                .ReturnsAsync(basket);

            //Act
            await sut.Handle(productPriceChangedEvent);

            //Assert
            mockBasketRepository.Verify(_ => _.UpdateBasketAsync(
                    It.IsAny<CustomerBasket>()
                ), 
                Times.Never
            );
        }

        [Theory]
        [AutoMoqData]
        public async Task Handle_BasketNotFound_BasketNotUpdated(
            [Frozen] Mock<IBasketRepository> mockBasketRepository,
            ProductPriceChangedIntegrationEventHandler sut,
            ProductPriceChangedIntegrationEvent productPriceChangedEvent,
            string user1
        )
        {
            //Arrange
            mockBasketRepository.Setup(_ => _.GetUsers())
                .Returns(new string[] { user1 });

            mockBasketRepository.Setup(_ => _.GetBasketAsync(It.Is<string>(_ => _ == user1)))
                .ReturnsAsync((CustomerBasket?)null);

            //Act
            await sut.Handle(productPriceChangedEvent);

            //Assert
            mockBasketRepository.Verify(_ => _.UpdateBasketAsync(
                    It.IsAny<CustomerBasket>()
                ),
                Times.Never
            );
        }
    }
}