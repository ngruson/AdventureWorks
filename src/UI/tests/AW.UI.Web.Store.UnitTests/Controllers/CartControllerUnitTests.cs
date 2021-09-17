using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Store.Controllers;
using AW.UI.Web.Store.Services;
using AW.UI.Web.Store.ViewModels;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace AW.UI.Web.Store.UnitTests.Controllers
{
    public class CartControllerUnitTests
    {
        public class Index
        {
            [Theory, AutoMoqData]
            public async Task Index_ReturnsBasketViewModel(
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
            public async Task Index_ThrowsException(
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
    }
}