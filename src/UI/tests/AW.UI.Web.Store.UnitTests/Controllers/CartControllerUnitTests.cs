using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Store.Controllers;
using AW.UI.Web.Store.Services;
using AW.UI.Web.Store.ViewModels;
using AW.UI.Web.Store.ViewModels.Cart;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace AW.UI.Web.Store.UnitTests.Controllers
{
    public class CartControllerUnitTests
    {
        public class IndexGet
        {
            [Theory, AutoMoqData]
            public async Task IndexGet_ReturnsBasketViewModel(
                [Frozen] Mock<IBasketService> mockBasketService,
                [Frozen] Mock<ITempDataDictionaryFactory> mockDataDict,
                [Greedy] CartController sut,
                Basket basket
            )
            {
                //Arrange
                mockBasketService.Setup(_ => _.GetBasketAsync(It.IsAny<string>()))
                    .ReturnsAsync(basket);

                var serviceProvider = new Mock<IServiceProvider>();
                serviceProvider.Setup(_ => _.GetService(typeof(ITempDataDictionaryFactory)))
                    .Returns(mockDataDict.Object);

                sut.ControllerContext.HttpContext = new DefaultHttpContext
                {
                    RequestServices = serviceProvider.Object
                };

                //Act
                var actionResult = await sut.Index();

                //Assert
                var viewResult = actionResult.Should().BeAssignableTo<ViewResult>().Subject;
                viewResult.Model.Should().BeEquivalentTo(basket);
            }

            [Theory, AutoMoqData]
            public async Task IndexGet_ThrowsException(
                [Greedy] CartController sut
            )
            {
                //Arrange

                //Act
                var actionResult = await sut.Index();

                //Assert
                var viewResult = actionResult.Should().BeAssignableTo<ViewResult>().Subject;
                viewResult.Model.Should().BeNull();
                var msg = viewResult.ViewData["BasketInoperativeMsg"].Should().BeAssignableTo<string>().Subject;
                msg.Should().Match(_ => _.StartsWith("Basket Service is inoperative"));
            }
        }

        public class IndexPost
        {
            [Theory, AutoMoqData]
            public async Task IndexPost_ActionCheckout_RedirectToCreateOrder(
                [Greedy] CartController sut,
                Dictionary<string, int> quantities,
                ClaimsPrincipal user
            )
            {
                //Arrange
                sut.ControllerContext.HttpContext = new DefaultHttpContext
                {
                    User = user
                };

                //Act
                var actionResult = await sut.Index(quantities, "Checkout");

                //Assert
                var redirectActionResult = actionResult.Should().BeAssignableTo<RedirectToActionResult>().Subject;
                redirectActionResult.ControllerName.Should().Be("Order");
                redirectActionResult.ActionName.Should().Be("Create");
            }

            [Theory, AutoMoqData]
            public async Task IndexPost_OtherAction_RedirectToCreateOrder(
                [Greedy] CartController sut,
                Dictionary<string, int> quantities,
                ClaimsPrincipal user,
                string actionName
            )
            {
                //Arrange
                sut.ControllerContext.HttpContext = new DefaultHttpContext
                {
                    User = user
                };

                //Act
                var actionResult = await sut.Index(quantities, actionName);

                //Assert
                var redirectActionResult = actionResult.Should().BeAssignableTo<RedirectToActionResult>().Subject;
                redirectActionResult.ControllerName.Should().BeNull();
                redirectActionResult.ActionName.Should().Be("Index");
            }

            [Theory, AutoMoqData]
            public async Task IndexPost_ThrowsException(
                [Greedy] CartController sut,
                Dictionary<string, int> quantities,
                string actionName
            )
            {
                //Arrange

                //Act
                var actionResult = await sut.Index(quantities, actionName);

                //Assert
                var redirectActionResult = actionResult.Should().BeAssignableTo<RedirectToActionResult>().Subject;
                redirectActionResult.ControllerName.Should().BeNull();
                redirectActionResult.ActionName.Should().Be("Index");
            }
        }

        public class AddToCart
        {
            [Theory, AutoMoqData]
            public async Task AddToCart_ProductNumber_ProductAddedToBasket(
                [Frozen] Mock<IBasketService> mockBasketService,
                [Greedy] CartController sut,
                string productNumber,
                ClaimsPrincipal user
            )
            {
                //Arrange
                sut.ControllerContext.HttpContext = new DefaultHttpContext
                {
                    User = user
                };

                //Act
                var actionResult = await sut.AddToCart(productNumber);

                //Assert
                var redirectActionResult = actionResult.Should().BeAssignableTo<RedirectToActionResult>().Subject;
                redirectActionResult.ControllerName.Should().Be("Home");
                redirectActionResult.ActionName.Should().Be("Index");

                mockBasketService.Verify(_ => _.AddBasketItemAsync(
                    It.IsAny<ApplicationUser>(),
                    It.IsAny<string>(),
                    It.IsAny<int>()
                ));
            }

            [Theory, AutoMoqData]
            public async Task AddToCart_NoProductNumber_NotAddedToBasket(
                [Frozen] Mock<IBasketService> mockBasketService,
                [Greedy] CartController sut
            )
            {
                //Arrange

                //Act
                var actionResult = await sut.AddToCart(null);

                //Assert
                var redirectActionResult = actionResult.Should().BeAssignableTo<RedirectToActionResult>().Subject;
                redirectActionResult.ControllerName.Should().Be("Home");
                redirectActionResult.ActionName.Should().Be("Index");

                mockBasketService.Verify(_ => _.AddBasketItemAsync(
                    It.IsAny<ApplicationUser>(),
                    It.IsAny<string>(),
                    It.IsAny<int>()
                ), Times.Never);
            }

            [Theory, AutoMoqData]
            public async Task AddToCart_ThrowsException(
                [Greedy] CartController sut,
                string productNumber
            )
            {
                //Arrange

                //Act
                var actionResult = await sut.AddToCart(productNumber);

                //Assert
                var redirectActionResult = actionResult.Should().BeAssignableTo<RedirectToActionResult>().Subject;
                redirectActionResult.ControllerName.Should().Be("Home");
                redirectActionResult.ActionName.Should().Be("Index");
                redirectActionResult.RouteValues.ContainsKey("errorMsg").Should().Be(true);
            }
        }
    }
}