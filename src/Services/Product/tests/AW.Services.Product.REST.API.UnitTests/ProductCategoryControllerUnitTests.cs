using AutoFixture.Xunit2;
using AW.Services.Product.Core.Handlers.GetProductCategories;
using AW.Services.Product.REST.API.Controllers;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace AW.Services.Product.REST.API.UnitTests
{
    public class ProductCategoryControllerUnitTests
    {
        public class GetProductCategories
        {
            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task GetProductCategories_ShouldReturnProductCategories_WhenProductCategoriesExist(
                [Frozen] List<ProductCategory> categories,
                [Frozen] Mock<IMediator> mockMediator,
                [Greedy] ProductCategoryController sut
                )
            {
                //Arrange
                mockMediator.Setup(x => x.Send(
                    It.IsAny<GetProductCategoriesQuery>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(categories);

                //Act
                var actionResult = await sut.GetProductCategories();

                //Assert
                var okObjectResult = actionResult as OkObjectResult;
                okObjectResult.Should().NotBeNull();

                var response = okObjectResult?.Value as List<ProductCategory>;
                response?.Count.Should().Be(categories.Count);

                for (int i = 0; i < response?.Count; i++)
                {
                    response[i].Name.Should().Be(categories[i].Name);
                }
            }
        }
    }
}