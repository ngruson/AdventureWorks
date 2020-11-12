using AW.Application.Product.GetProduct;
using AW.Application.Product.GetProducts;
using AW.ProductService.Messages;
using FluentAssertions;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AW.ProductService.UnitTests
{
    [TestClass]
    public class ProductServiceUnitTests
    {
        [TestMethod]
        public async Task ListCustomers_ReturnsCustomers()
        {
            //Arrange
            var dto = new List<Application.Product.GetProducts.ProductDto>
            {
                new Application.Product.GetProducts.ProductDto { ProductNumber = "1" },
                new Application.Product.GetProducts.ProductDto { ProductNumber = "2" }
            };

            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(x => x.Send(It.IsAny<GetProductsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(dto);
            var productService = new ProductService(
                mockMediator.Object
            );

            //Act
            var request = new ListProductsRequest();
            var result = await productService.ListProducts(request);

            //Assert
            result.Should().NotBeNull();
            result.Products.Count().Should().Be(2);
        }

        [TestMethod]
        public async Task GetProduct_ReturnsProduct()
        {
            //Arrange
            var dto = new Application.Product.GetProduct.ProductDto { ProductNumber = "1" };

            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(x => x.Send(It.IsAny<GetProductQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(dto);
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