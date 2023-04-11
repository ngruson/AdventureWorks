using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Admin.Mvc.Controllers;
using AW.UI.Web.Admin.Mvc.Services;
using AW.UI.Web.Admin.Mvc.ViewModels.ProductModel;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace AW.UI.Web.Admin.Mvc.UnitTests.Controllers
{
    public class ProductModelControllerUnitTests
    {
        public class Index
        {
            [Theory, AutoMoqData]
            public async Task Index_ReturnsViewModel(
                [Frozen] Mock<IProductModelService> productModelService,
                [Greedy] List<ProductModelViewModel> productModels,
                [Greedy] ProductModelController sut
            )
            {
                //Arrange
                productModelService.Setup(x => x.GetProductModels())
                    .ReturnsAsync(productModels);

                //Act
                var actionResult = await sut.Index();

                //Assert
                var viewResult = actionResult.Should().BeAssignableTo<ViewResult>().Subject;
                viewResult.Model.Should().Be(productModels);
            }
        }
    }
}
