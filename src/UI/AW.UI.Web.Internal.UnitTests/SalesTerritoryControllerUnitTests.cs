using AW.UI.Web.Internal.Controllers;
using AW.UI.Web.Internal.Interfaces;
using AW.UI.Web.Internal.ViewModels.SalesTerritory;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace AW.UI.Web.Internal.UnitTests
{
    public class SalesTerritoryControllerUnitTests
    {
        public class Index
        {
            [Fact]
            public async void Index_ReturnsViewModel()
            {
                //Arrange
                var mockSalesTerritoryViewModelService = new Mock<ISalesTerritoryViewModelService>();
                mockSalesTerritoryViewModelService.Setup(x => x.GetSalesTerritories())
                .ReturnsAsync(new SalesTerritoryIndexViewModel());

                var controller = new SalesTerritoryController(
                    mockSalesTerritoryViewModelService.Object
                );

                //Act
                var actionResult = await controller.Index();

                //Assert
                var viewResult = actionResult.Should().BeAssignableTo<ViewResult>().Subject;
                viewResult.Model.Should().BeAssignableTo<SalesTerritoryIndexViewModel>();

                mockSalesTerritoryViewModelService.Verify(x => x.GetSalesTerritories());
            }
        }
    }
}