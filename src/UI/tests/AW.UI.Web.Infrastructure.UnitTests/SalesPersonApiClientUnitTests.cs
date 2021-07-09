using AW.UI.Web.Infrastructure.ApiClients.SalesPersonApi;
using AW.UI.Web.Infrastructure.UnitTests.TestBuilders.GetSalesPersons;
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

namespace AW.UI.Web.Infrastructure.UnitTests
{
    public class SalesPersonApiClientUnitTests
    {
        public class GetSalesPersons
        {
            [Fact]
            public async void GetSalesPersons_SalesPersonsFound_ReturnsSalesPersons()
            {
                //Arrange

                var mockLogger = new Mock<ILogger<SalesPersonApiClient>>();

                var salesPersons = new List<Infrastructure.ApiClients.SalesPersonApi.Models.SalesPerson>
                {
                    new SalesPersonBuilder()
                        .WithTestValues()
                        .Build(),
                    new SalesPersonBuilder()
                        .FirstName("Michael")
                        .MiddleName("G")
                        .LastName("Blythe")
                        .Territory("Norhteast")
                        .Build()
                };

                var httpClient = new HttpClient(new HttpMessageHandlerStub(async (request, cancellationToken) =>
                {
                    var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent(
                            JsonSerializer.Serialize(salesPersons, new JsonSerializerOptions
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
                var sut = new SalesPersonApiClient(httpClient, mockLogger.Object);
                var response = await sut.GetSalesPersonsAsync();

                //Assert
                response.Should().NotBeNull();
                response.Should().BeEquivalentTo(salesPersons);
            }

            [Fact]
            public void GetSalesPersons_NoSalesPersonsFound_ThrowsHttpRequestException()
            {
                //Arrange

                var mockLogger = new Mock<ILogger<SalesPersonApiClient>>();

                var httpClient = new HttpClient(new HttpMessageHandlerStub(async (request, cancellationToken) =>
                {
                    var responseMessage = new HttpResponseMessage(HttpStatusCode.NotFound);
                    return await Task.FromResult(responseMessage);
                }))
                {
                    BaseAddress = new Uri("http://baseaddress")
                };

                //Act
                var sut = new SalesPersonApiClient(httpClient, mockLogger.Object);
                Func<Task> func = async () => await sut.GetSalesPersonsAsync();

                //Assert
                func.Should().Throw<HttpRequestException>()
                    .WithMessage("Response status code does not indicate success: 404 (Not Found).");
            }
        }

        public class GetSalesPerson
        {
            [Fact]
            public async void GetSalesPerson_SalesPersonFound_ReturnsSalesPerson()
            {
                //Arrange

                var mockLogger = new Mock<ILogger<SalesPersonApiClient>>();

                var salesPerson = new SalesPersonBuilder()
                    .WithTestValues()
                    .Build();

                var httpClient = new HttpClient(new HttpMessageHandlerStub(async (request, cancellationToken) =>
                {
                    var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent(
                            JsonSerializer.Serialize(salesPerson, new JsonSerializerOptions
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
                var sut = new SalesPersonApiClient(httpClient, mockLogger.Object);
                var response = await sut.GetSalesPersonAsync("Stephen", "Y", "Jiang");

                //Assert
                response.Should().NotBeNull();
                response.Should().BeEquivalentTo(salesPerson);
            }

            [Fact]
            public void GetSalesPerson_NoSalesPersonFound_ThrowsHttpRequestException()
            {
                //Arrange

                var mockLogger = new Mock<ILogger<SalesPersonApiClient>>();

                var httpClient = new HttpClient(new HttpMessageHandlerStub(async (request, cancellationToken) =>
                {
                    var responseMessage = new HttpResponseMessage(HttpStatusCode.NotFound);
                    return await Task.FromResult(responseMessage);
                }))
                {
                    BaseAddress = new Uri("http://baseaddress")
                };

                //Act
                var sut = new SalesPersonApiClient(httpClient, mockLogger.Object);
                Func<Task> func = async () => await sut.GetSalesPersonAsync("Stephen", "Y", "Jiang");

                //Assert
                func.Should().Throw<HttpRequestException>()
                    .WithMessage("Response status code does not indicate success: 404 (Not Found).");
            }
        }
    }
}