using AW.UI.Web.Infrastructure.ApiClients.ProductApi.Models;
using AW.UI.Web.Store.Controllers;
using AW.UI.Web.Store.Services;
using AW.UI.Web.Store.ViewModels.Home;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace AW.UI.Web.Store.UnitTests.Controllers
{
    public class HomeControllerUnitTests
    {
        [Fact]
        public async Task Index_ReturnsHomeViewModel()
        {
            //Arrange
            var mockProductService = new Mock<IProductService>();
            mockProductService.Setup(x => x.GetCategoriesAsync())
                .ReturnsAsync(new List<ProductCategory>
                {
                    new ProductCategory { Name = "Bikes" },
                    new ProductCategory { Name = "Components" }
                });
                
            //Act
            var controller = new HomeController(mockProductService.Object);
            var actionResult = await controller.Index();

            //Assert
            var viewResult = actionResult.Should().BeAssignableTo<ViewResult>().Subject;
            var homeViewModel = viewResult.Model.Should().BeAssignableTo<HomeViewModel>().Subject;
            homeViewModel.ProductCategories.Should().NotBeNull();
            homeViewModel.ProductCategories.Count.Should().Be(2);
        }
    }
}