using AutoFixture.Xunit2;
using AW.Services.Product.Core.Handlers.GetProduct;
using AW.Services.Product.Core.Handlers.GetProducts;
using AW.Services.Product.REST.API.Controllers;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace AW.Services.Product.REST.API.UnitTests
{
    public class ProductControllerUnitTests
    {
        public class GetProducts
        {
            [Theory, AutoMapperData(typeof(MappingProfile), typeof(Core.AutoMapper.MappingProfile))]
            public async Task GetProducts_ShouldReturnProducts_WhenProductsExist(
                [Frozen] Mock<IMediator> mockMediator,
                List<Core.Handlers.GetProduct.Product> products,
                [Greedy] ProductController sut,
                GetProductsQuery query
            )
            {
                //Arrange
                var dto = new GetProductsDto
                {
                    Products = products,
                    TotalProducts = products.Count
                };

                mockMediator.Setup(x => x.Send(It.IsAny<GetProductsQuery>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(dto);

                //Act
                var actionResult = await sut.GetProducts(query);

                //Assert
                var okObjectResult = actionResult as OkObjectResult;
                okObjectResult.Should().NotBeNull();

                var response = okObjectResult?.Value as Models.GetProductsResult;
                response?.TotalProducts.Should().Be(products.Count);
                response?.Products!.Count.Should().Be(products.Count);

                for (int i = 0; i < products.Count; i++)
                {
                    response?.Products![i].ProductNumber.Should().Be(products[i].ProductNumber);
                }
            }

            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task GetProducts_ShouldReturnNotFound_WhenNoProductsFound(
                [Greedy] ProductController sut,
                GetProductsQuery query
            )
            {
                //Act
                var actionResult = await sut.GetProducts(query);

                //Assert
                var okObjectResult = actionResult as NotFoundResult;
                okObjectResult.Should().NotBeNull();
            }
        }

        public class GetProduct
        {
            [Theory, AutoMapperData(typeof(MappingProfile), typeof(Core.AutoMapper.MappingProfile))]
            public async Task GetProduct_ShouldReturnProduct_WhenProductExist(
                [Frozen] Core.Handlers.GetProduct.Product product,
                [Frozen] Mock<IMediator> mockMediator,
                [Greedy] ProductController sut,
                GetProductQuery query
            )
            {
                //Arrange
                mockMediator.Setup(x => x.Send(It.IsAny<GetProductQuery>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(product);

                //Act
                var actionResult = await sut.GetProduct(query);

                //Assert
                var okObjectResult = actionResult as OkObjectResult;
                okObjectResult.Should().NotBeNull();

                var response = okObjectResult?.Value as Core.Models.Product;
                response?.ProductNumber.Should().Be(product.ProductNumber);
            }

            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task GetProduct_ShouldReturnNotFound_WhenProductNotFound(
                [Frozen] Mock<IMediator> mockMediator,
                [Greedy] ProductController sut,
                GetProductQuery query
            )
            {
                //Arrange
#pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
                mockMediator.Setup(x => x.Send(
                        It.IsAny<GetProductQuery>(), 
                        It.IsAny<CancellationToken>()
                    )
                )
                .ReturnsAsync((Core.Handlers.GetProduct.Product?)null);
#pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.

                //Act
                var actionResult = await sut.GetProduct(query);

                //Assert
                var okObjectResult = actionResult as NotFoundResult;
                okObjectResult.Should().NotBeNull();
            }
        }
    }
}