using AutoMapper;
using AW.Services.Product.Core.Handlers.GetProduct;
using AW.Services.Product.Core.Handlers.GetProducts;
using AW.Services.Product.REST.API.Controllers;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.Product.REST.API.UnitTests
{
    public class ProductControllerUnitTests
    {
        public class GetProducts
        {
            [Fact]
            public async Task GetProducts_ShouldReturnProducts_WhenProductsExist()
            {
                //Arrange
                var mapper = new MapperConfiguration(opts =>
                {
                    opts.AddProfile<MappingProfile>();
                }).CreateMapper();

                var dto = new GetProductsDto
                {
                    TotalProducts = 2,
                    Products = new List<Core.Handlers.GetProducts.Product>
                    {
                        new Core.Handlers.GetProducts.Product { ProductNumber = "BB-7421" },
                        new Core.Handlers.GetProducts.Product { ProductNumber = "BB-8107" }
                    }
                };

                var mockLogger = new Mock<ILogger<ProductController>>();
                var mockMediator = new Mock<IMediator>();
                mockMediator.Setup(x => x.Send(It.IsAny<GetProductsQuery>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(dto);

                var controller = new ProductController(
                    mockLogger.Object,
                    mockMediator.Object,
                    mapper
                );

                //Act
                var request = new GetProductsQuery();
                var actionResult = await controller.GetProducts(request);

                //Assert
                var okObjectResult = actionResult as OkObjectResult;
                okObjectResult.Should().NotBeNull();

                var response = okObjectResult.Value as Models.GetProductsResult;
                response.TotalProducts.Should().Be(2);
                response.Products.Count.Should().Be(2);
                response.Products[0].ProductNumber.Should().Be("BB-7421");
                response.Products[1].ProductNumber.Should().Be("BB-8107");
            }

            [Fact]
            public async Task GetProducts_ShouldReturnNotFound_WhenNoProductsFound()
            {
                //Arrange
                var mapper = new MapperConfiguration(opts =>
                {
                    opts.AddProfile<MappingProfile>();
                }).CreateMapper();

                var mockLogger = new Mock<ILogger<ProductController>>();
                var mockMediator = new Mock<IMediator>();

                var controller = new ProductController(
                    mockLogger.Object,
                    mockMediator.Object,
                    mapper
                );

                //Act
                var request = new GetProductsQuery();
                var actionResult = await controller.GetProducts(request);

                //Assert
                var okObjectResult = actionResult as NotFoundResult;
                okObjectResult.Should().NotBeNull();
            }
        }

        public class GetProduct
        {
            [Fact]
            public async Task GetProduct_ShouldReturnProduct_WhenProductExist()
            {
                //Arrange
                var mapper = new MapperConfiguration(opts =>
                {
                    opts.AddProfile<MappingProfile>();
                }).CreateMapper();

                var dto = new Core.Handlers.GetProduct.Product
                {
                    ProductNumber = "BB-7421"
                };

                var mockLogger = new Mock<ILogger<ProductController>>();
                var mockMediator = new Mock<IMediator>();
                mockMediator.Setup(x => x.Send(It.IsAny<GetProductQuery>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(dto);

                var controller = new ProductController(
                    mockLogger.Object,
                    mockMediator.Object,
                    mapper
                );

                //Act
                var query = new GetProductQuery
                {
                    ProductNumber = "BB-7421"
                };
                var actionResult = await controller.GetProduct(query);

                //Assert
                var okObjectResult = actionResult as OkObjectResult;
                okObjectResult.Should().NotBeNull();

                var response = okObjectResult.Value as Models.Product;
                response.ProductNumber.Should().Be("BB-7421");
            }

            [Fact]
            public async Task GetProduct_ShouldReturnNotFound_WhenProductNotFound()
            {
                //Arrange
                var mapper = new MapperConfiguration(opts =>
                {
                    opts.AddProfile<MappingProfile>();
                }).CreateMapper();

                var mockLogger = new Mock<ILogger<ProductController>>();
                var mockMediator = new Mock<IMediator>();

                var controller = new ProductController(
                    mockLogger.Object,
                    mockMediator.Object,
                    mapper
                );

                //Act
                var query = new GetProductQuery();
                var actionResult = await controller.GetProduct(query);

                //Assert
                var okObjectResult = actionResult as NotFoundResult;
                okObjectResult.Should().NotBeNull();
            }
        }
    }
}