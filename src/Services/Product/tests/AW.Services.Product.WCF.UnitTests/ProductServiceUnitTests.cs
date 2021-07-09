using AW.Services.Product.Core.Handlers.GetProduct;
using AW.Services.Product.Core.Handlers.GetProducts;
using AW.Services.Product.WCF.Messages;
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
        [Fact]
        public async Task ListProducts_ReturnsProducts()
        {
            //Arrange
            var products = new GetProductsDto
            {
                Products = new List<Core.Handlers.GetProducts.Product>
                {
                    new Core.Handlers.GetProducts.Product { ProductNumber = "1" },
                    new Core.Handlers.GetProducts.Product { ProductNumber = "2" }
                },
                TotalProducts = 2
            };

            var mockMediator = new Mock<IMediator>();

            mockMediator.Setup(x => x.Send(It.IsAny<GetProductsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(products);
            var productService = new ProductService(
                mockMediator.Object
            );

            //Act
            var request = new ListProductsRequest();
            var result = await productService.ListProducts(request);

            //Assert
            result.Should().NotBeNull();
            result.Products.Product.Count().Should().Be(2);
        }

        [Fact]
        public async Task GetProduct_ReturnsProduct()
        {
            //Arrange
            var product = new Core.Handlers.GetProduct.Product { ProductNumber = "AR-5381" };

            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(x => x.Send(It.IsAny<GetProductQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(product);
            var productService = new ProductService(
                mockMediator.Object
            );

            //Act
            var request = new GetProductRequest
            {
                ProductNumber = "AR-5381"
            };
            var result = await productService.GetProduct(request);

            //Assert
            result.Should().NotBeNull();
            result.Product.Should().NotBeNull();
            result.Product.ProductNumber.Should().Be("AR-5381");
        }
    }
}