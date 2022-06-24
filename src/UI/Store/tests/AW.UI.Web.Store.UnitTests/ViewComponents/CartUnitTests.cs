using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Store.ViewComponents;
using AW.UI.Web.Store.ViewModels;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using AW.UI.Web.Store.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using AW.UI.Web.Store.ViewModels.Cart;
using System;

namespace AW.UI.Web.Store.UnitTests.ViewComponents
{
    public class CartUnitTests
    {
        [Theory, AutoMoqData]
        public async Task InvokeAsync_BasketExists_ReturnsCartComponentViewModel(
            [Frozen] Mock<IBasketService> mockBasketService,
            [Greedy] Cart sut,
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
            var viewModel = viewResult.ViewData.Model.Should().BeAssignableTo<CartComponentViewModel>().Subject;
            viewModel.ItemsCount.Should().Be(basket.Items.Count);
            viewModel.Disabled.Should().BeNullOrEmpty();
        }

        [Theory, AutoMoqData]
        public async Task InvokeAsync_Exception_ReturnsCartComponentViewModel(
            [Frozen] Mock<IBasketService> mockBasketService,
            [Greedy] Cart sut,
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
            var viewModel = viewResult.ViewData.Model.Should().BeAssignableTo<CartComponentViewModel>().Subject;            
            viewModel.ItemsCount.Should().Be(0);
            viewModel.Disabled.Should().Be("is-disabled");
            viewResult.ViewData["IsBasketInoperative"].Should().Be(true);

            mockBasketService.Verify(_ => _.GetBasketAsync<Basket>(It.IsAny<string>()));
        }
    }
}
