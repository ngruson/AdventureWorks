using AW.UI.Web.Internal.ApiClients.SalesOrderApi;
using AW.UI.Web.Internal.UnitTests.TestBuilders.GetSalesOrders;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Xunit;

namespace AW.UI.Web.Internal.UnitTests
{
    public class SalesOrderApiClientUnitTests
    {
        public class GetSalesOrders
        {
            [Fact]
            public async void GetSalesOrders_SalesOrdersFound_ReturnsSalesOrders()
            {
                //Arrange

                var mockLogger = new Mock<ILogger<SalesOrderApiClient>>();

                var salesOrders = new ApiClients.SalesOrderApi.Models.SalesOrdersResult
                {
                    SalesOrders = new List<ApiClients.SalesOrderApi.Models.SalesOrder>
                    {
                        new SalesOrderBuilder()
                            .WithTestValues()
                            .Build()
                    },
                    TotalSalesOrders = 1
                };

                var httpClient = new HttpClient(new HttpMessageHandlerStub(async (request, cancellationToken) =>
                {
                    var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent(
                            JsonSerializer.Serialize(salesOrders, new JsonSerializerOptions
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
                var sut = new SalesOrderApiClient(httpClient, mockLogger.Object);
                var response = await sut.GetSalesOrdersAsync(0, 10, null, null);

                //Assert
                response.Should().NotBeNull();
                response.TotalSalesOrders.Should().Be(1);
                response.Should().BeEquivalentTo(salesOrders);
            }

            [Fact]
            public void GetSalesOrders_NoSalesOrdersFound_ThrowsHttpRequestException()
            {
                //Arrange

                var mockLogger = new Mock<ILogger<SalesOrderApiClient>>();

                var httpClient = new HttpClient(new HttpMessageHandlerStub(async (request, cancellationToken) =>
                {
                    var responseMessage = new HttpResponseMessage(HttpStatusCode.NotFound);
                    return await Task.FromResult(responseMessage);
                }))
                {
                    BaseAddress = new Uri("http://baseaddress")
                };

                //Act
                var sut = new SalesOrderApiClient(httpClient, mockLogger.Object);
                Func<Task> func = async () => await sut.GetSalesOrdersAsync(0, 10, null, null);

                //Assert
                func.Should().Throw<HttpRequestException>()
                    .WithMessage("Response status code does not indicate success: 404 (Not Found).");
            }
        }

        public class GetSalesOrder
        {
            [Fact]
            public async void GetSalesOrder_SalesOrderFound_ReturnsSalesOrder()
            {
                //Arrange

                var mockLogger = new Mock<ILogger<SalesOrderApiClient>>();

                var salesOrder = new SalesOrderBuilder()
                    .WithTestValues()
                    .Build();

                var httpClient = new HttpClient(new HttpMessageHandlerStub(async (request, cancellationToken) =>
                {
                    var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent(
                            JsonSerializer.Serialize(salesOrder, new JsonSerializerOptions
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
                var sut = new SalesOrderApiClient(httpClient, mockLogger.Object);
                var response = await sut.GetSalesOrderAsync("SO43659");

                //Assert
                response.Should().NotBeNull();
                response.Should().BeEquivalentTo(salesOrder);
            }

            [Fact]
            public void GetSalesOrder_NoSalesOrderFound_ThrowsHttpRequestException()
            {
                //Arrange

                var mockLogger = new Mock<ILogger<SalesOrderApiClient>>();

                var httpClient = new HttpClient(new HttpMessageHandlerStub(async (request, cancellationToken) =>
                {
                    var responseMessage = new HttpResponseMessage(HttpStatusCode.NotFound);
                    return await Task.FromResult(responseMessage);
                }))
                {
                    BaseAddress = new Uri("http://baseaddress")
                };

                //Act
                var sut = new SalesOrderApiClient(httpClient, mockLogger.Object);
                Func<Task> func = async () => await sut.GetSalesOrderAsync("SO43659");

                //Assert
                func.Should().Throw<HttpRequestException>()
                    .WithMessage("Response status code does not indicate success: 404 (Not Found).");
            }
        }
    }
}