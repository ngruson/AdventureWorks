using AutoFixture.Xunit2;
using AW.Services.Product.Core;
using AW.Services.Product.Core.Handlers.GetProduct;
using AW.Services.Product.Core.Handlers.GetProducts;
using AW.Services.Product.WCF.Messages;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using MediatR;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.Product.WCF.UnitTests
{
    public class ProductServiceUnitTests
    {
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task ListProducts_ReturnsProducts(
            [Frozen] Mock<IMediator> mockMediator,
            List<Core.Handlers.GetProduct.Product> products,
            ProductService sut,
            ListProductsRequest request
        )
        {
            //Arrange
            var dto = new GetProductsDto
            {
                Products = products,
                TotalProducts = products.Count
            };

            mockMediator.Setup(x => x.Send(
                It.IsAny<GetProductsQuery>(), 
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(dto);

            //Act
            var result = await sut.ListProducts(request);

            //Assert
            result.Should().NotBeNull();
            result.Products.Product.Count().Should().Be(products.Count);
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task GetProduct_ReturnsProduct(
            [Frozen] Core.Handlers.GetProduct.Product product,
            [Frozen] Mock<IMediator> mockMediator,
            ProductService sut,
            GetProductRequest request
        )
        {
            //Arrange
            mockMediator.Setup(x => x.Send(
                It.IsAny<GetProductQuery>(), 
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(product);

            //Act
            var result = await sut.GetProduct(request);

            //Assert
            result.Should().NotBeNull();
            result.Product.Should().NotBeNull();
            result.Product.ProductNumber.Should().Be(product.ProductNumber);
        }
    }
}