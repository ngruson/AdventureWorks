using AW.Core.Abstractions.Api.ProductApi.GetProduct;
using AW.Core.Abstractions.Api.ProductApi.ListProducts;
using AW.Infrastructure.Http;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Xunit;

namespace AW.Infrastructure.Api.REST.UnitTests
{
    public class ProductApiUnitTests
    {
        [Fact]
        public async void ListProducts_ReturnsProducts()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<ProductApi>>();
            var mockHttpRequestFactory = new Mock<IHttpRequestFactory>();
            mockHttpRequestFactory.Setup(x => x.Get(
                    It.IsAny<string>(), It.IsAny<Dictionary<string, string>>(), It.IsAny<string>()
                )
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new JsonContent(new ListProductsResponse
                {
                    Products = new List<Core.Abstractions.Api.ProductApi.ListProducts.Product>
                    {
                        new Core.Abstractions.Api.ProductApi.ListProducts.Product { ProductNumber = "ST-1401" },
                        new Core.Abstractions.Api.ProductApi.ListProducts.Product { ProductNumber = "ST-1402" },
                    }
                })
            });

            var baseAddress = "BaseAddress";

            var sut = new ProductApi(
                mockLogger.Object,
                mockHttpRequestFactory.Object,
                baseAddress
            );

            //Act
            var response = await sut.ListProductsAsync(new ListProductsRequest());

            //Assert
            mockHttpRequestFactory.Verify(x => x.Get(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>(), It.IsAny<string>()));
            response.Products[0].ProductNumber.Should().Be("ST-1401");
            response.Products[1].ProductNumber.Should().Be("ST-1402");
        }

        [Fact]
        public async void GetProduct_ReturnsProduct()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<ProductApi>>();
            var mockHttpRequestFactory = new Mock<IHttpRequestFactory>();
            mockHttpRequestFactory.Setup(x => x.Get(
                    It.IsAny<string>(), It.IsAny<Dictionary<string, string>>(), It.IsAny<string>()
                )
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new JsonContent(new GetProductResponse
                {
                    Product = new Core.Abstractions.Api.ProductApi.GetProduct.Product
                    {
                        ProductNumber = "ST-1401"
                    }
                })
            });

            var baseAddress = "BaseAddress";

            var sut = new ProductApi(
                mockLogger.Object,
                mockHttpRequestFactory.Object,
                baseAddress
            );

            //Act
            var response = await sut.GetProduct(new GetProductRequest());

            //Assert
            mockHttpRequestFactory.Verify(x => x.Get(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>(), It.IsAny<string>()));
            response.Product.ProductNumber.Should().Be("ST-1401");
        }
    }
}