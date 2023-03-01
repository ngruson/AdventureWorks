using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Store.Mvc.Services;
using AW.UI.Web.Store.Mvc.ViewModels;
using AW.UI.Web.Store.Mvc.ViewModels.Cart;
using AW.UI.Web.Store.Mvc.Views.Product;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Moq;
using Xunit;

namespace AW.UI.Web.Store.Mvc.UnitTests.ViewComponents
{
    public class CartListUnitTests
    {
        [Theory, AutoMoqData]
        public async Task InvokeAsync_BasketExists_ReturnsBasket(
            [Frozen] Mock<IBasketService> mockBasketService,
            [Greedy] CartList sut,
            Basket basket,
            ApplicationUser user
        )
        {
            //Arrange
            mockBasketService.Setup(_ => _.GetBasketAsync<Basket>(It.IsAny<string>()))
                .ReturnsAsync(basket);

            //Act
            var result = await sut.InvokeAsync(user);

            //Assert
            var viewResult = result.Should().BeAssignableTo<ViewViewComponentResult>().Subject;
            var viewModel = viewResult?.ViewData?.Model.Should().Be(basket);
        }

        [Theory, AutoMoqData]
        public async Task InvokeAsync_Exception_ReturnsBasket(
            [Frozen] Mock<IBasketService> mockBasketService,
            [Greedy] CartList sut,
            ApplicationUser user
        )
        {
            //Arrange
            mockBasketService.Setup(_ => _.GetBasketAsync<Basket>(It.IsAny<string>()))
                .Throws<Exception>();

            //Act
            var result = await sut.InvokeAsync(user);

            //Assert
            var viewResult = result.Should().BeAssignableTo<ViewViewComponentResult>().Subject;
            var viewModel = viewResult?.ViewData?.Model.Should().BeOfType<Basket>();
            var msg = viewResult?.ViewData?["BasketInoperativeMsg"].Should().BeAssignableTo<string>().Subject;
            msg.Should().Match(_ => _.StartsWith("Basket Service is inoperative"));
        }
    }
}
