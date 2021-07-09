using AW.Common.JsonConverters;
using AW.UI.Web.Common.ApiClients.CustomerApi;
using AW.UI.Web.Common.ApiClients.CustomerApi.Models.GetCustomers;
using AW.UI.Web.Common.ApiClients.ProductApi;
using AW.UI.Web.Common.ApiClients.ProductApi.Models;
using AW.UI.Web.Common.UnitTests.TestBuilders;
using AW.UI.Web.Common.UnitTests.TestBuilders.GetCustomers;
using AW.UI.Web.Common.UnitTests.TestBuilders.Product;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Xunit;

namespace AW.UI.Web.Common.UnitTests
{
    public class ProductApiClientUnitTests
    {
        public class GetProducts
        {
            [Fact]
            public async void GetProducts_ProductsFound_ReturnsProducts()
            {
                //Arrange

                var mockLogger = new Mock<ILogger<ProductApiClient>>();

                var customers = new GetProductsResult
                {
                    Products = new List<Product>
                    {
                        new ProductBuilder()
                            .WithTestValues()
                            .Build()
                    },
                    TotalProducts = 1
                };

                var httpClient = new HttpClient(new HttpMessageHandlerStub(async (request, cancellationToken) =>
                {
                    var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent(
                            JsonSerializer.Serialize(customers, new JsonSerializerOptions
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
                    };

                    return await Task.FromResult(responseMessage);
                }))
                {
                    BaseAddress = new Uri("http://baseaddress")
                };

                //Act
                var sut = new ProductApiClient(httpClient, mockLogger.Object);
                var response = await sut.GetProductsAsync(0, 10, null, null, null);

                //Assert
                response.Should().NotBeNull();
                response.TotalProducts.Should().Be(1);
                var product = response.Products[0];
                product.Name.Should().Be("LL Bottom Bracket");
                product.ProductNumber.Should().Be("BB-7421");
                product.Color.Should().BeNull();
                product.ListPrice.Should().Be(53.99M);
                product.Size.Should().BeNull();
                product.SizeUnitMeasureCode.Should().BeNull();
                product.Weight.Should().Be(223);
                product.WeightUnitMeasureCode.Should().Be("G");
                product.ProductLine.Should().BeNull();
                product.Class.Should().Be("L");
                product.Style.Should().BeNull();
                product.ProductCategoryName.Should().Be("Components");
                product.ProductSubcategoryName.Should().Be("Bottom Brackets");
                product.ThumbnailPhoto.Should().BeNull();
                product.LargePhoto.Should().BeNull();
            }

            [Fact]
            public void GetProducts_NoProductsFound_ThrowsHttpRequestException()
            {
                //Arrange
                var mockLogger = new Mock<ILogger<ProductApiClient>>();

                var httpClient = new HttpClient(new HttpMessageHandlerStub(async (request, cancellationToken) =>
                {
                    var responseMessage = new HttpResponseMessage(HttpStatusCode.NotFound);
                    return await Task.FromResult(responseMessage);
                }))
                {
                    BaseAddress = new Uri("http://baseaddress")
                };

                //Act
                var sut = new ProductApiClient(httpClient, mockLogger.Object);
                Func<Task> func = async () => await sut.GetProductsAsync(0, 10, null, null, null);

                //Assert
                func.Should().Throw<HttpRequestException>()
                    .WithMessage("Response status code does not indicate success: 404 (Not Found).");
            }
        }

        public class GetProductCategories
        {
            [Fact]
            public async void GetCategories_CategoriesFound_ReturnsCategories()
            {
                //Arrange

                var mockLogger = new Mock<ILogger<ProductApiClient>>();

                var categories = new List<ProductCategory>
                {
                    new ProductCategoryBuilder()
                        .WithTestValues()
                        .Build()
                };

                var httpClient = new HttpClient(new HttpMessageHandlerStub(async (request, cancellationToken) =>
                {
                    var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent(
                            JsonSerializer.Serialize(categories, new JsonSerializerOptions
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
                    };

                    return await Task.FromResult(responseMessage);
                }))
                {
                    BaseAddress = new Uri("http://baseaddress")
                };

                //Act
                var sut = new ProductApiClient(httpClient, mockLogger.Object);
                var response = await sut.GetCategoriesAsync();

                //Assert
                response.Should().NotBeNull();
                response.Count.Should().Be(1);
                response[0].Name.Should().Be("Components");
                response[0].Subcategories[0].Name.Should().Be("Bottom Brackets");
                response[0].Subcategories[0].ProductCount.Should().Be(1);
            }
        }
    }
}