using AW.UI.Web.Admin.Mvc.Controllers;
using AW.UI.Web.Admin.Mvc.ViewModels.SalesPerson;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;
using AW.UI.Web.Admin.Mvc.Services;

namespace AW.UI.Web.Admin.Mvc.UnitTests.Controllers
{
    public class SalesPersonControllerUnitTests
    {
        public class Index
        {
            [Fact]
            public async Task Index_ReturnsViewModel()
            {
                //Arrange
                var mockSalesPersonViewModelService = new Mock<ISalesPersonViewModelService>();
                mockSalesPersonViewModelService.Setup(x => x.GetSalesPersons(
                    It.IsAny<string>())
                )
                .ReturnsAsync(new SalesPersonIndexViewModel());

                var controller = new SalesPersonController(
                    mockSalesPersonViewModelService.Object
                );

                //Act
                var actionResult = await controller.Index();

                //Assert
                var viewResult = actionResult.Should().BeAssignableTo<ViewResult>().Subject;
                viewResult.Model.Should().BeAssignableTo<SalesPersonIndexViewModel>();

                mockSalesPersonViewModelService.Verify(x => x.GetSalesPersons(
                    It.IsAny<string>())
                );
            }
        }
    }
}