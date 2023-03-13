using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Store.Mvc.Services;
using AW.UI.Web.Store.Mvc.ViewModels;
using FluentAssertions;
using Moq;
using Xunit;
using MediatR;
using AW.UI.Web.SharedKernel.Customer.Handlers.GetPreferredAddress;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using AW.UI.Web.Store.Mvc;

namespace AW.UI.Web.Store.Mvc.UnitTests.Services
{
    public class BasketServiceUnitTests
    {
        public class GetBasket
        {
            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task GetBasket_BasketExist_ReturnsBasket(
                [Frozen] Mock<IMediator> mockMediator,
                [Greedy] BasketService sut,
                SharedKernel.Basket.Handlers.GetBasket.Basket basket
            )
            {
                //Arrange
                mockMediator.Setup(_ => _.Send(
                        It.IsAny<SharedKernel.Basket.Handlers.GetBasket.GetBasketQuery>(),
                        It.IsAny<CancellationToken>()
                    )
                )
                .ReturnsAsync(basket);

                //Act
                var response = await sut.GetBasketAsync<SharedKernel.Basket.Handlers.GetBasket.Basket>(basket.BuyerId);

                //Assert
                mockMediator.Verify(_ => _.Send(
                        It.IsAny<SharedKernel.Basket.Handlers.GetBasket.GetBasketQuery>(),
                        It.IsAny<CancellationToken>()
                    )
                );

                response.Should().Be(basket);
            }
        }

        public class AddBasketItem
        {
            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task AddBasketItem_ProductNotExistsInBasket_ReturnsBasket(
                [Frozen] Mock<IMediator> mockMediator,
                [Greedy] BasketService sut,
                ApplicationUser user,
                SharedKernel.Basket.Handlers.GetBasket.Basket basket,
                SharedKernel.Product.Handlers.GetProduct.Product product,
                int quantity
            )
            {
                //Arrange

                var i = 0;
                product.ProductProductPhotos?.ForEach(photo =>
                    {
                        photo.Primary = i == 0;
                        i++;
                    });
                
                mockMediator.Setup(_ => _.Send(
                        It.IsAny<SharedKernel.Product.Handlers.GetProduct.GetProductQuery>(),
                        It.IsAny<CancellationToken>()
                    )
                )
                .ReturnsAsync(product);

                mockMediator.Setup(_ => _.Send(
                        It.IsAny<SharedKernel.Basket.Handlers.GetBasket.GetBasketQuery>(),
                        It.IsAny<CancellationToken>()
                    )
                )
                .ReturnsAsync(basket);

                //Act
                var response = await sut.AddBasketItemAsync(
                    user,
                    product.ProductNumber,
                    quantity
                );

                //Assert
                response.Should().Be(basket);
                mockMediator.Verify(_ => _.Send(
                        It.IsAny<SharedKernel.Basket.Handlers.UpdateBasket.UpdateBasketCommand>(),
                        It.IsAny<CancellationToken>()
                    )
                );
            }

            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task AddBasketItem_ProductExistsInBasket_ReturnsBasket(
                [Frozen] Mock<IMediator> mockMediator,
                [Greedy] BasketService sut,
                ApplicationUser user,
                SharedKernel.Basket.Handlers.GetBasket.Basket basket,
                SharedKernel.Product.Handlers.GetProduct.Product product,
                int quantity
            )
            {
                //Arrange
                mockMediator.Setup(_ => _.Send(
                        It.IsAny<SharedKernel.Product.Handlers.GetProduct.GetProductQuery>(),
                        It.IsAny<CancellationToken>()
                    )
                )
                .ReturnsAsync(product);

                mockMediator.Setup(_ => _.Send(
                        It.IsAny<SharedKernel.Basket.Handlers.GetBasket.GetBasketQuery>(),
                        It.IsAny<CancellationToken>()
                    )
                )
                .ReturnsAsync(basket);

                basket.Items.Add(new SharedKernel.Basket.Handlers.GetBasket.BasketItem
                {
                    ProductNumber = product.ProductNumber,
                    Quantity = 10
                });

                //Act
                var response = await sut.AddBasketItemAsync(
                    user,
                    product.ProductNumber,
                    quantity
                );

                //Assert
                response.BuyerId.Should().Be(basket.BuyerId);
                response.Items.Count.Should().Be(basket.Items.Count);
                response.Items.Where(_ => _.ProductNumber == product.ProductNumber)
                    .ToList()[0].Quantity.Should().Be(10 + quantity);

                mockMediator.Verify(_ => _.Send(
                        It.IsAny<SharedKernel.Basket.Handlers.UpdateBasket.UpdateBasketCommand>(),
                        It.IsAny<CancellationToken>()
                    )
                );
            }

            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task AddBasketItem_ProductDoesNotExist_ThrowsArgumentNullException(
                [Frozen] Mock<IMediator> mockMediator,
                [Greedy] BasketService sut,
                ApplicationUser user,
                SharedKernel.Product.Handlers.GetProduct.Product product,
                int quantity
            )
            {
                //Arrange
                mockMediator.Setup(_ => _.Send(
                        It.IsAny<SharedKernel.Product.Handlers.GetProduct.GetProductQuery>(),
                        It.IsAny<CancellationToken>()
                    )
                )
                .ReturnsAsync(null as SharedKernel.Product.Handlers.GetProduct.Product);

                //Act
                Func<Task> func = async () => await sut.AddBasketItemAsync(
                    user,
                    product.ProductNumber,
                    quantity
                );

                //Assert
                await func.Should().ThrowAsync<ArgumentNullException>()
                    .WithMessage("Value cannot be null. (Parameter 'product')");

                mockMediator.Verify(_ => _.Send(
                        It.IsAny<SharedKernel.Basket.Handlers.UpdateBasket.UpdateBasketCommand>(),
                        It.IsAny<CancellationToken>()
                    ),
                    Times.Never
                );
            }
        }

        public class SetQuantities
        {
            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task SetQuantities_ProductExistsInBasket_ReturnsBasket(
                [Frozen] Mock<IMediator> mockMediator,
                [Greedy] BasketService sut,
                ApplicationUser user,
                SharedKernel.Basket.Handlers.GetBasket.Basket basket,
                List<int> quantityValues
            )
            {
                //Arrange
                Dictionary<string, int> quantities = new();
                for (var i = 0; i < basket.Items.Count; i++)
                {
                    quantities.Add(basket.Items[i].Id!, quantityValues[i]);
                }

                mockMediator.Setup(_ => _.Send(
                        It.IsAny<SharedKernel.Basket.Handlers.GetBasket.GetBasketQuery>(),
                        It.IsAny<CancellationToken>()
                    )
                )
                .ReturnsAsync(basket);

                //Act
                var resultBasket = await sut.SetQuantities(
                    user,
                    quantities
                );

                //Assert
                for (var i = 0; i < basket.Items.Count; i++)
                {
                    resultBasket.Items[i].Quantity.Should().Be(quantityValues[i]);
                }
                mockMediator.Verify(_ => _.Send(
                        It.IsAny<SharedKernel.Basket.Handlers.UpdateBasket.UpdateBasketCommand>(),
                        It.IsAny<CancellationToken>()
                    )
                );
            }
        }

        public class Checkout
        {
            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task Checkout_GetPreferrredAddressIsCalledForBillingAndShipping(
                [Frozen] Mock<IBasketApiClient> mockClient,
                [Frozen] Mock<IMediator> mockMediator,
                [Greedy] BasketService sut,
                SharedKernel.Basket.Handlers.GetBasket.Basket basket,
                ApplicationUser user
            )
            {
                //Arrange
                mockClient.Setup(_ => _.GetBasketAsync(It.IsAny<string>()))
                    .ReturnsAsync(basket);

                //Act
                var response = await sut.Checkout(user);

                //Assert
                mockMediator.Verify(_ => _.Send(
                        It.Is<GetPreferredAddressQuery>(_ =>
                            _.AccountNumber == user.CustomerNumber && _.AddressType == "Billing"
                        ),
                        It.IsAny<CancellationToken>()
                    )
                );

                mockMediator.Verify(_ => _.Send(
                        It.Is<GetPreferredAddressQuery>(_ =>
                            _.AccountNumber == user.CustomerNumber && _.AddressType == "Shipping"
                        ),
                        It.IsAny<CancellationToken>()
                    )
                );
            }
        }
    }
}
