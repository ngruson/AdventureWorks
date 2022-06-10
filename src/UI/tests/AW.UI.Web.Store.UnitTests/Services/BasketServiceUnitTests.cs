using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Infrastructure.ApiClients.BasketApi;
using bm = AW.UI.Web.Infrastructure.ApiClients.BasketApi.Models;
using AW.UI.Web.Infrastructure.ApiClients.ProductApi;
using AW.UI.Web.Infrastructure.ApiClients.ProductApi.Models;
using AW.UI.Web.Store.Services;
using AW.UI.Web.Store.ViewModels;
using AW.UI.Web.Store.ViewModels.Cart;
using FluentAssertions;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using System.Collections.Generic;

namespace AW.UI.Web.Store.UnitTests.Services
{
    public class BasketServiceUnitTests
    {
        public class GetBasket
        {
            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task GetBasket_BasketExist_ReturnsBasket(
                [Frozen] Mock<IBasketApiClient> mockClient,
                [Greedy] BasketService sut,
                bm.Basket basket
            )
            {
                //Arrange
                mockClient.Setup(_ => _.GetBasket(It.IsAny<string>()))
                    .ReturnsAsync(basket);

                //Act
                var response = await sut.GetBasketAsync<Basket>(basket.BuyerId);

                //Assert
                response.BuyerId.Should().Be(basket.BuyerId);
                response.Items.Count.Should().Be(basket.Items.Count);
            }
        }

        public class AddBasketItem
        {
            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task AddBasketItem_ProductNotExistsInBasket_ReturnsBasket(
                [Frozen] Mock<IBasketApiClient> mockBasketApiClient,
                [Frozen] Mock<IProductApiClient> mockProductApiClient,
                [Greedy] BasketService sut,
                ApplicationUser user,
                bm.Basket basket,
                Product product,
                int quantity
            )
            {
                //Arrange
                mockProductApiClient.Setup(_ => _.GetProductAsync(It.IsAny<string>()))
                    .ReturnsAsync(product);

                mockBasketApiClient.Setup(_ => _.GetBasket(It.IsAny<string>()))
                    .ReturnsAsync(basket);                

                //Act
                var response = await sut.AddBasketItemAsync(
                    user,
                    product.ProductNumber,
                    quantity
                );

                //Assert
                response.BuyerId.Should().Be(basket.BuyerId);
                response.Items.Count.Should().Be(basket.Items.Count + 1);
                mockBasketApiClient.Verify(_ => _.UpdateBasket(It.IsAny<bm.Basket>()));
            }

            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task AddBasketItem_ProductExistsInBasket_ReturnsBasket(
                [Frozen] Mock<IBasketApiClient> mockBasketApiClient,
                [Frozen] Mock<IProductApiClient> mockProductApiClient,
                [Greedy] BasketService sut,
                ApplicationUser user,
                bm.Basket basket,
                Product product,
                int quantity
            )
            {
                //Arrange
                mockProductApiClient.Setup(_ => _.GetProductAsync(It.IsAny<string>()))
                    .ReturnsAsync(product);

                mockBasketApiClient.Setup(_ => _.GetBasket(It.IsAny<string>()))
                    .ReturnsAsync(basket);

                basket.Items.Add(new bm.BasketItem
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

                mockBasketApiClient.Verify(_ => _.UpdateBasket(It.IsAny<bm.Basket>()));
            }
        }

        public class SetQuantities
        {
            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task AddBasketItem_ProductExistsInBasket_ReturnsBasket(
                [Frozen] Mock<IBasketApiClient> mockBasketApiClient,
                [Greedy] BasketService sut,
                ApplicationUser user,
                bm.Basket basket,
                List<int> quantityValues
            )
            {
                //Arrange
                Dictionary<string, int> quantities = new();
                for (int i = 0; i < basket.Items.Count; i++)
                {
                    quantities.Add(basket.Items[i].Id, quantityValues[i]);
                }

                mockBasketApiClient.Setup(_ => _.GetBasket(It.IsAny<string>()))
                    .ReturnsAsync(basket);

                //Act
                var resultBasket = await sut.SetQuantities(
                    user,
                    quantities
                );

                //Assert
                for (int i = 0; i < basket.Items.Count; i++)
                {
                    resultBasket.Items[i].Quantity.Should().Be(quantityValues[i]);
                }
                mockBasketApiClient.Verify(_ => _.UpdateBasket(It.IsAny<bm.Basket>()));
            }
        }

        public class Checkout
        {
            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task Checkout_GetPreferrredAddressIsCalledForBillingAndShipping(
                [Frozen] Mock<IBasketApiClient> mockClient,
                [Frozen] Mock<ICustomerService> mockCustomerService,
                [Greedy] BasketService sut,
                bm.Basket basket,
                ApplicationUser user
            )
            {
                //Arrange
                mockClient.Setup(_ => _.GetBasket(It.IsAny<string>()))
                    .ReturnsAsync(basket);

                //Act
                var response = await sut.Checkout(user);

                //Assert
                mockCustomerService.Verify(_ => _.GetPreferredAddressAsync(
                    It.Is<string>(_ => _ == user.CustomerNumber),
                    It.Is<string>(_ => _ == "Billing")
                ));

                mockCustomerService.Verify(_ => _.GetPreferredAddressAsync(
                    It.Is<string>(_ => _ == user.CustomerNumber),
                    It.Is<string>(_ => _ == "Shipping")
                ));
            }
        }
    }
}