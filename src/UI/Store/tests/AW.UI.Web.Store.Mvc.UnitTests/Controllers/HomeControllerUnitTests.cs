using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Infrastructure.Api.Product.Handlers.GetProductCategories;
using AW.UI.Web.Store.Mvc.Controllers;
using AW.UI.Web.Store.Mvc.ViewModels.Home;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace AW.UI.Web.Store.Mvc.UnitTests.Controllers
{
    public class HomeControllerUnitTests
    {
        [Theory, AutoMoqData]
        public async Task Index_ReturnsHomeViewModel(
            [Frozen] Mock<IMediator> mockMediator,
            List<ProductCategory> categories
        )
        {
            //Arrange
            mockMediator.Setup(x => x.Send(
                    It.IsAny<GetProductCategoriesQuery>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(categories);

            var sut = new HomeController(
                mockMediator.Object
            );

            //Act
            var actionResult = await sut.Index();

            //Assert
            var viewResult = actionResult.Should().BeAssignableTo<ViewResult>().Subject;
            var homeViewModel = viewResult.Model.Should().BeAssignableTo<HomeViewModel>().Subject;
            homeViewModel.ProductCategories.Should().BeEquivalentTo(categories);
        }
    }
}
