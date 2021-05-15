using AW.Services.Product.Application.GetProduct;
using AW.Services.Product.Application.GetProducts;
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
        public async Task ListCustomers_ReturnsCustomers()
        {
            //Arrange
            var products = new GetProductsDto
            {
                Products = new List<Application.GetProducts.Product>
                {
                    new Application.GetProducts.Product { ProductNumber = "1" },
                    new Application.GetProducts.Product { ProductNumber = "2" }
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
            var product = new Application.GetProduct.Product { ProductNumber = "1" };

            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(x => x.Send(It.IsAny<GetProductQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(product);
            var productService = new ProductService(
                mockMediator.Object
            );

            //Act
            var request = new GetProductRequest();
            var result = await productService.GetProduct(request);

            //Assert
            result.Should().NotBeNull();
            result.Product.Should().NotBeNull();
        }
    }
}