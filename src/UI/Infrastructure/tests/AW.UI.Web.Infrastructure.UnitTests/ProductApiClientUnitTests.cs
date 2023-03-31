using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Infrastructure.ApiClients;
using AW.UI.Web.SharedKernel.Product.Handlers.GetProductCategories;
using AW.UI.Web.SharedKernel.Product.Handlers.GetProducts;
using FluentAssertions;
using RichardSzalay.MockHttp;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Xunit;

namespace AW.UI.Web.Infrastructure.UnitTests
{
    public class ProductApiClientUnitTests
    {
        public class GetProducts
        {
            [Theory, MockHttpData]
            public async Task GetProducts_ProductsFound_ReturnsProducts(
                [Frozen] MockHttpMessageHandler handler,
                [Frozen] HttpClient httpClient,
                Uri uri,
                List<Product> list,
                ProductApiClient sut,
                string category, 
                string subCategory,
                string orderBy
            )
            {
                //Arrange
                var products = new GetProductsResult
                {
                    Products = list,
                    TotalProducts = list.Count
                };

                httpClient.BaseAddress = uri;
                handler.When(HttpMethod.Get, $"{uri}*")
                    .Respond(HttpStatusCode.OK,
                        new StringContent(
                            JsonSerializer.Serialize(products, new JsonSerializerOptions
                            {
                                Converters =
                                {
                                    new JsonStringEnumConverter()
                                },
                                IgnoreReadOnlyProperties = true,
                                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                            })
                        )
                    );

                //Act
                var response = await sut.GetProducts(0, 10, category, subCategory, orderBy);

                //Assert
                response.Should().NotBeNull();
                response?.TotalProducts.Should().Be(list.Count);
                response?.Products.Should().BeEquivalentTo(list);
            }

            [Theory, MockHttpData]
            public async Task GetProducts_NoProductsFound_ThrowsHttpRequestException(
                [Frozen] MockHttpMessageHandler handler,
                [Frozen] HttpClient httpClient,
                Uri uri,
                ProductApiClient sut
            )
            {
                //Arrange
                httpClient.BaseAddress = uri;

                handler.When(HttpMethod.Get, $"{uri}*")
                    .Respond(HttpStatusCode.NotFound);

                //Act
                Func<Task> func = async () => await sut.GetProducts(0, 10, null, null, null);

                //Assert
                await func.Should().ThrowAsync<HttpRequestException>()
                    .WithMessage("Response status code does not indicate success: 404 (Not Found).");
            }
        }

        public class GetProduct
        {
            [Theory, MockHttpData]
            public async Task GetProduct_ProductFound_ReturnsProduct(
                [Frozen] MockHttpMessageHandler handler,
                [Frozen] HttpClient httpClient,
                Uri uri,
                Product product,
                ProductApiClient sut
            )
            {
                //Arrange
                httpClient.BaseAddress = uri;
                handler.When(HttpMethod.Get, $"{uri}*")
                    .Respond(HttpStatusCode.OK,
                        new StringContent(
                            JsonSerializer.Serialize(product, new JsonSerializerOptions
                            {
                                Converters =
                                {
                                    new JsonStringEnumConverter()
                                },
                                IgnoreReadOnlyProperties = true,
                                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                            })
                        )
                    );

                //Act
                var response = await sut.GetProduct(product.ProductNumber);

                //Assert
                response.Should().NotBeNull();
                response.Should().BeEquivalentTo(product);
            }

            [Theory, MockHttpData]
            public async Task GetProduct_NoProductFound_ThrowsHttpRequestException(
                [Frozen] MockHttpMessageHandler handler,
                [Frozen] HttpClient httpClient,
                Uri uri,
                ProductApiClient sut,
                string productNumber
            )
            {
                //Arrange
                httpClient.BaseAddress = uri;

                handler.When(HttpMethod.Get, $"{uri}*")
                    .Respond(HttpStatusCode.NotFound);

                //Act
                Func<Task> func = async () => await sut.GetProduct(productNumber);

                //Assert
                await func.Should().ThrowAsync<HttpRequestException>()
                    .WithMessage("Response status code does not indicate success: 404 (Not Found).");
            }
        }

        public class GetProductCategories
        {
            [Theory, MockHttpData]
            public async Task GetCategories_CategoriesFound_ReturnsCategories(
                [Frozen] MockHttpMessageHandler handler,
                [Frozen] HttpClient httpClient,
                Uri uri,
                List<ProductCategory> categories,
                ProductApiClient sut
            )
            {
                //Arrange
                httpClient.BaseAddress = uri;

                handler.When(HttpMethod.Get, $"{uri}*")
                    .Respond(HttpStatusCode.OK,
                        new StringContent(
                            JsonSerializer.Serialize(categories, 
                                new JsonSerializerOptions
                                {
                                    Converters =
                                    {
                                        new JsonStringEnumConverter()
                                    },
                                    IgnoreReadOnlyProperties = true,
                                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                                }),
                                Encoding.UTF8,
                                "application/json"
                            )
                        );

                //Act
                var response = await sut.GetCategories();

                //Assert
                response.Should().BeEquivalentTo(categories);
            }
        }

        public class UpdateProduct
        {
            [Theory, MockHttpData]
            public async Task ReturnUpdatedProductGivenProduct(
                [Frozen] MockHttpMessageHandler handler,
                [Frozen] HttpClient httpClient,
                Uri uri,
                ProductApiClient sut,
                SharedKernel.Product.Handlers.UpdateProduct.Product product
            )
            {
                //Arrange
                httpClient.BaseAddress = uri;
                handler.When(HttpMethod.Put, $"{uri}*")
                    .Respond(HttpStatusCode.OK,
                        new StringContent(
                            JsonSerializer.Serialize(product, new JsonSerializerOptions
                            {
                                Converters =
                                {
                                    new JsonStringEnumConverter()
                                },
                                IgnoreReadOnlyProperties = true,
                                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                            })
                        )
                    );

                //Act
                var response = await sut.UpdateProduct(product.ProductNumber!, product);

                //Assert
                response.Should().BeEquivalentTo(product);
            }

            [Theory, MockHttpData]
            public async Task ThrowHttpRequestExceptionGivenProductNotFound(
                [Frozen] MockHttpMessageHandler handler,
                [Frozen] HttpClient httpClient,
                Uri uri,
                ProductApiClient sut,
                SharedKernel.Product.Handlers.UpdateProduct.Product product
            )
            {
                //Arrange
                httpClient.BaseAddress = uri;
                handler.When(HttpMethod.Put, $"{uri}*")
                    .Respond(HttpStatusCode.NotFound);

                //Act
                Func<Task> func = async () => await sut.UpdateProduct(product.ProductNumber!, product);

                //Assert
                await func.Should().ThrowAsync<HttpRequestException>()
                    .WithMessage("Response status code does not indicate success: 404 (Not Found).");
            }
        }

        public class DeleteProduct
        {
            [Theory, MockHttpData]
            public async Task ReturnUpdatedProductGivenProduct(
                [Frozen] MockHttpMessageHandler handler,
                [Frozen] HttpClient httpClient,
                Uri uri,
                ProductApiClient sut,
                string productNumber
            )
            {
                //Arrange
                httpClient.BaseAddress = uri;
                handler.When(HttpMethod.Delete, $"{uri}*")
                    .Respond(HttpStatusCode.OK);

                //Act
                await sut.DeleteProduct(productNumber);

                //Assert
                1.Should().Be(1);
            }

            [Theory, MockHttpData]
            public async Task ThrowHttpRequestExceptionGivenProductNotFound(
                [Frozen] MockHttpMessageHandler handler,
                [Frozen] HttpClient httpClient,
                Uri uri,
                ProductApiClient sut,
                string productNumber
            )
            {
                //Arrange
                httpClient.BaseAddress = uri;
                handler.When(HttpMethod.Delete, $"{uri}*")
                    .Respond(HttpStatusCode.NotFound);

                //Act
                Func<Task> func = async () => await sut.DeleteProduct(productNumber);

                //Assert
                await func.Should().ThrowAsync<HttpRequestException>()
                    .WithMessage("Response status code does not indicate success: 404 (Not Found).");
            }
        }

        public class DuplicateProduct
        {
            [Theory, MockHttpData]
            public async Task DuplicateProductGivenProductNumber(
                [Frozen] MockHttpMessageHandler handler,
                [Frozen] HttpClient httpClient,
                Uri uri,
                ProductApiClient sut,
                SharedKernel.Product.Handlers.DuplicateProduct.Product product
            )
            {
                //Arrange
                httpClient.BaseAddress = uri;
                handler.When(HttpMethod.Post, $"{uri}*")
                    .Respond(HttpStatusCode.OK,
                        new StringContent(
                            JsonSerializer.Serialize(product, new JsonSerializerOptions
                            {
                                Converters =
                                {
                                    new JsonStringEnumConverter()
                                },
                                IgnoreReadOnlyProperties = true,
                                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                            })
                        )
                    );

                //Act
                var response = await sut.DuplicateProduct(product.ProductNumber!);

                //Assert
                response.Should().BeEquivalentTo(product);
            }

            [Theory, MockHttpData]
            public async Task ThrowHttpRequestExceptionGivenProductNotFound(
                [Frozen] MockHttpMessageHandler handler,
                [Frozen] HttpClient httpClient,
                Uri uri,
                ProductApiClient sut,
                SharedKernel.Product.Handlers.UpdateProduct.Product product
            )
            {
                //Arrange
                httpClient.BaseAddress = uri;
                handler.When(HttpMethod.Put, $"{uri}*")
                    .Respond(HttpStatusCode.NotFound);

                //Act
                Func<Task> func = async () => await sut.UpdateProduct(product.ProductNumber!, product);

                //Assert
                await func.Should().ThrowAsync<HttpRequestException>()
                    .WithMessage("Response status code does not indicate success: 404 (Not Found).");
            }
        }
    }
}
