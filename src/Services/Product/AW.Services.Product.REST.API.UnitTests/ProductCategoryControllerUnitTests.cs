using AW.Services.Product.Application.GetProductCategories;
using AW.Services.Product.REST.API.Controllers;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.Product.REST.API.UnitTests
{
    public class ProductCategoryControllerUnitTests
    {
        public class GetProductCategories
        {
            [Fact]
            public async Task GetProductCategories_ShouldReturnProductCategories_WhenProductCategoriesExist()
            {
                //Arrange
                var dto = new List<ProductCategory>
                {
                    new ProductCategory { Name = "Bikes" },
                    new ProductCategory { Name = "Components" }
                };

                var mockLogger = new Mock<ILogger<ProductCategoryController>>();
                var mockMediator = new Mock<IMediator>();
                mockMediator.Setup(x => x.Send(It.IsAny<GetProductCategoriesQuery>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(dto);

                var controller = new ProductCategoryController(
                    mockLogger.Object,
                    mockMediator.Object
                );

                //Act
                var actionResult = await controller.GetProductCategories();

                //Assert
                var okObjectResult = actionResult as OkObjectResult;
                okObjectResult.Should().NotBeNull();

                var response = okObjectResult.Value as List<ProductCategory>;
                response.Count.Should().Be(2);
                response[0].Name.Should().Be("Bikes");
                response[1].Name.Should().Be("Components");
            }
        }
    }
}