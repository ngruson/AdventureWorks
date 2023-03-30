using AutoFixture.Xunit2;
using AW.Services.Infrastructure.ActionResults;
using AW.Services.Product.Core.Exceptions;
using AW.Services.Product.Core.Handlers.DuplicateProduct;
using AW.Services.Product.Core.Handlers.GetProduct;
using AW.Services.Product.Core.Handlers.GetProducts;
using AW.Services.Product.Core.Handlers.UpdateProduct;
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
            public async Task ReturnProductsWhenProductsExist(
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

                var response = okObjectResult?.Value as GetProductsDto;
                response?.TotalProducts.Should().Be(products.Count);
                response?.Products!.Count.Should().Be(products.Count);

                for (int i = 0; i < products.Count; i++)
                {
                    response?.Products![i].ProductNumber.Should().Be(products[i].ProductNumber);
                }
            }

            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task ReturnNotFoundWhenProductsDoNotExist(
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
            public async Task ReturnProductWhenProductExists(
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

                var response = okObjectResult?.Value as Core.Handlers.GetProduct.Product;
                response?.ProductNumber.Should().Be(product.ProductNumber);
            }

            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task ReturnNotFoundWhenProductDoesNotExist(
                [Frozen] Mock<IMediator> mockMediator,
                [Greedy] ProductController sut,
                GetProductQuery query
            )
            {
                //Arrange
                mockMediator.Setup(x => x.Send(
                        It.IsAny<GetProductQuery>(),
                        It.IsAny<CancellationToken>()
                    )
                )
                .ThrowsAsync(new ProductNotFoundException(query.ProductNumber!));

                //Act
                var actionResult = await sut.GetProduct(query);

                //Assert
                actionResult.Should().BeOfType<NotFoundResult>();
            }
        }

        public class UpdateProduct
        {
            [Theory, AutoMapperData(typeof(MappingProfile), typeof(Core.AutoMapper.MappingProfile))]
            public async Task ReturnProductWhenProductExists(
                [Frozen] Mock<IMediator> mockMediator,
                [Greedy] ProductController sut,
                Core.Handlers.UpdateProduct.Product product
            )
            {
                //Arrange
                mockMediator.Setup(x => x.Send(
                    It.IsAny<UpdateProductCommand>(), 
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(product);

                //Act
                var actionResult = await sut.UpdateProduct(product.ProductNumber!, product);

                //Assert
                var okObjectResult = actionResult as OkObjectResult;
                okObjectResult.Should().NotBeNull();

                var response = okObjectResult?.Value as Core.Handlers.UpdateProduct.Product;
                response?.ProductNumber.Should().Be(product.ProductNumber);
            }

            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task ReturnNotFoundWhenProductDoesNotExist(
                [Frozen] Mock<IMediator> mockMediator,
                [Greedy] ProductController sut,
                Core.Handlers.UpdateProduct.Product product
            )
            {
                //Arrange
                mockMediator.Setup(x => x.Send(
                        It.IsAny<UpdateProductCommand>(),
                        It.IsAny<CancellationToken>()
                    )
                )
                .ThrowsAsync(new ProductNotFoundException(product.ProductNumber!));

                //Act
                var actionResult = await sut.UpdateProduct(product.ProductNumber!, product);

                //Assert
                actionResult.Should().BeOfType<NotFoundResult>();
            }
        }

        public class DuplicateProduct
        {
            [Theory, AutoMapperData(typeof(MappingProfile), typeof(Core.AutoMapper.MappingProfile))]
            public async Task ReturnOkObjectResultWhenDuplicateSucceeds(
                [Frozen] Mock<IMediator> mediator,
                [Greedy] ProductController sut,
                DuplicateProductCommand command,
                Core.Handlers.DuplicateProduct.Product product
            )
            {
                //Arrange
                mediator.Setup(_ => _.Send(
                        It.IsAny<DuplicateProductCommand>(),
                        It.IsAny<CancellationToken>()
                    )
                )
                .ReturnsAsync(product);

                //Act
                var actionResult = await sut.DuplicateProduct(command);

                //Assert
                var okObjectResult = actionResult.Should().BeAssignableTo<OkObjectResult>().Subject;
                var returnedProduct = okObjectResult.Value as Core.Handlers.DuplicateProduct.Product;
                returnedProduct.Should().Be(product);
            }

            [Theory, AutoMapperData(typeof(MappingProfile), typeof(Core.AutoMapper.MappingProfile))]
            public async Task ReturnNotFoundResultWhenProductNotFoundExceptionIsThrown(
                [Frozen] Mock<IMediator> mockMediator,
                [Greedy] ProductController sut,
                DuplicateProductCommand command
            )
            {
                //Arrange
                mockMediator.Setup(_ => _.Send(
                    It.IsAny<DuplicateProductCommand>(),
                    It.IsAny<CancellationToken>()
                    )
                )
                .ThrowsAsync(new ProductNotFoundException(command.ProductNumber!));

                //Act
                var actionResult = await sut.DuplicateProduct(command);

                //Assert
                actionResult.Should().BeOfType<NotFoundResult>();
            }

            [Theory, AutoMapperData(typeof(MappingProfile), typeof(Core.AutoMapper.MappingProfile))]
            public async Task ReturnInternalServerErrorWhenDuplicateProductExceptionIsThrown(
                [Frozen] Mock<IMediator> mockMediator,
                [Greedy] ProductController sut,
                DuplicateProductCommand command
            )
            {
                //Arrange
                mockMediator.Setup(_ => _.Send(
                    It.IsAny<DuplicateProductCommand>(),
                    It.IsAny<CancellationToken>()
                    )
                )
                .ThrowsAsync(new DuplicateProductException(command.ProductNumber!, new ArgumentException()));

                //Act
                var actionResult = await sut.DuplicateProduct(command);

                //Assert
                actionResult.Should().BeOfType<InternalServerErrorObjectResult>();
            }
        }
    }
}
