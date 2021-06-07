using AW.UI.Web.Internal.Controllers;
using AW.UI.Web.Internal.Interfaces;
using AW.UI.Web.Internal.ViewModels.SalesPerson;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace AW.UI.Web.Internal.UnitTests
{
    public class SalesPersonControllerUnitTests
    {
        public class Index
        {
            [Fact]
            public async void Index_ReturnsViewModel()
            {
                //Arrange
                var mockSalesPersonViewModelService = new Mock<ISalesPersonViewModelService>();
                mockSalesPersonViewModelService.Setup(x => x.GetSalesPersons())
                .ReturnsAsync(new SalesPersonIndexViewModel());

                var controller = new SalesPersonController(
                    mockSalesPersonViewModelService.Object
                );

                //Act
                var actionResult = await controller.Index();

                //Assert
                var viewResult = actionResult.Should().BeAssignableTo<ViewResult>().Subject;
                viewResult.Model.Should().BeAssignableTo<SalesPersonIndexViewModel>();

                mockSalesPersonViewModelService.Verify(x => x.GetSalesPersons());
            }
        }
    }
}