using AW.UI.Web.Common.ApiClients.ReferenceDataApi;
using AW.UI.Web.Common.UnitTests.TestBuilders.GetAddressTypes;
using AW.UI.Web.Common.UnitTests.TestBuilders.GetContactTypes;
using AW.UI.Web.Common.UnitTests.TestBuilders.GetCountries;
using AW.UI.Web.Common.UnitTests.TestBuilders.GetStateProvinces;
using AW.UI.Web.Common.UnitTests.TestBuilders.GetTerritories;
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
using System.Threading.Tasks;
using Xunit;

namespace AW.UI.Web.Common.UnitTests
{
    public class ReferenceDataApiClientUnitTests
    {
        public class GetAddressTypes
        {
            [Fact]
            public async void GetAddressTypes_AddressTypesFound_ReturnsAddressTypes()
            {
                //Arrange

                var mockLogger = new Mock<ILogger<ReferenceDataApiClient>>();

                var addressTypes = (new string[] { "Billing", "Home", "Main Office", "Primary", "Shipping", "Archive" })
                    .Select(addressType => new AddressTypeBuilder()
                     .Name(addressType)
                     .Build()
                );

                var httpClient = new HttpClient(new HttpMessageHandlerStub(async (request, cancellationToken) =>
                {
                    var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent(
                            JsonSerializer.Serialize(addressTypes, new JsonSerializerOptions
                            {
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
                var sut = new ReferenceDataApiClient(httpClient, mockLogger.Object);
                var response = await sut.GetAddressTypesAsync();

                //Assert
                response.Should().NotBeNull();
                response.Count.Should().Be(6);
            }

            [Fact]
            public void GetAddressTypes_NoAddressTypesFound_ThrowsHttpRequestException()
            {
                //Arrange
                var mockLogger = new Mock<ILogger<ReferenceDataApiClient>>();

                var httpClient = new HttpClient(new HttpMessageHandlerStub(async (request, cancellationToken) =>
                {
                    var responseMessage = new HttpResponseMessage(HttpStatusCode.NotFound);
                    return await Task.FromResult(responseMessage);
                }))
                {
                    BaseAddress = new Uri("http://baseaddress")
                };

                //Act
                var sut = new ReferenceDataApiClient(httpClient, mockLogger.Object);
                Func<Task> func = async () => await sut.GetAddressTypesAsync();

                //Assert
                func.Should().Throw<HttpRequestException>()
                    .WithMessage("Response status code does not indicate success: 404 (Not Found).");
            }
        }

        public class GetContactTypes
        {
            [Fact]
            public async void GetContactTypes_ContactTypesFound_ReturnsContactTypes()
            {
                //Arrange

                var mockLogger = new Mock<ILogger<ReferenceDataApiClient>>();

                var contactTypes = (new string[] { "Account Manager", "Owner", "Order Administrator" })
                    .Select(contactType => new ContactTypeBuilder()
                     .Name(contactType)
                     .Build()
                );

                var httpClient = new HttpClient(new HttpMessageHandlerStub(async (request, cancellationToken) =>
                {
                    var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent(
                            JsonSerializer.Serialize(contactTypes, new JsonSerializerOptions
                            {
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
                var sut = new ReferenceDataApiClient(httpClient, mockLogger.Object);
                var response = await sut.GetContactTypesAsync();

                //Assert
                response.Should().NotBeNull();
                response.Count.Should().Be(3);
            }

            [Fact]
            public void GetContactTypes_NoContactTypesFound_ThrowsHttpRequestException()
            {
                //Arrange
                var mockLogger = new Mock<ILogger<ReferenceDataApiClient>>();

                var httpClient = new HttpClient(new HttpMessageHandlerStub(async (request, cancellationToken) =>
                {
                    var responseMessage = new HttpResponseMessage(HttpStatusCode.NotFound);
                    return await Task.FromResult(responseMessage);
                }))
                {
                    BaseAddress = new Uri("http://baseaddress")
                };

                //Act
                var sut = new ReferenceDataApiClient(httpClient, mockLogger.Object);
                Func<Task> func = async () => await sut.GetContactTypesAsync();

                //Assert
                func.Should().Throw<HttpRequestException>()
                    .WithMessage("Response status code does not indicate success: 404 (Not Found).");
            }
        }

        public class GetCountries
        {
            [Fact]
            public async void GetCountries_CountriesFound_ReturnsCountries()
            {
                //Arrange

                var mockLogger = new Mock<ILogger<ReferenceDataApiClient>>();

                var countries = (new string[] { "United States", "United Kingdom" })
                    .Select(country => new CountryBuilder()
                     .Name(country)
                     .Build()
                );

                var httpClient = new HttpClient(new HttpMessageHandlerStub(async (request, cancellationToken) =>
                {
                    var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent(
                            JsonSerializer.Serialize(countries, new JsonSerializerOptions
                            {
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
                var sut = new ReferenceDataApiClient(httpClient, mockLogger.Object);
                var response = await sut.GetCountriesAsync();

                //Assert
                response.Should().NotBeNull();
                response.Count.Should().Be(2);
            }

            [Fact]
            public void GetCountries_NoCountriesFound_ThrowsHttpRequestException()
            {
                //Arrange
                var mockLogger = new Mock<ILogger<ReferenceDataApiClient>>();

                var httpClient = new HttpClient(new HttpMessageHandlerStub(async (request, cancellationToken) =>
                {
                    var responseMessage = new HttpResponseMessage(HttpStatusCode.NotFound);
                    return await Task.FromResult(responseMessage);
                }))
                {
                    BaseAddress = new Uri("http://baseaddress")
                };

                //Act
                var sut = new ReferenceDataApiClient(httpClient, mockLogger.Object);
                Func<Task> func = async () => await sut.GetCountriesAsync();

                //Assert
                func.Should().Throw<HttpRequestException>()
                    .WithMessage("Response status code does not indicate success: 404 (Not Found).");
            }
        }

        public class GetStateProvinces
        {
            [Fact]
            public async void GetStateProvinces_StateProvincesFound_ReturnsStateProvinces()
            {
                //Arrange

                var mockLogger = new Mock<ILogger<ReferenceDataApiClient>>();

                var stateProvinces = (new string[] { "California", "Washington" })
                    .Select(country => new StateProvinceBuilder()
                     .Name(country)
                     .Build()
                );

                var httpClient = new HttpClient(new HttpMessageHandlerStub(async (request, cancellationToken) =>
                {
                    var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent(
                            JsonSerializer.Serialize(stateProvinces, new JsonSerializerOptions
                            {
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
                var sut = new ReferenceDataApiClient(httpClient, mockLogger.Object);
                var response = await sut.GetStateProvincesAsync();

                //Assert
                response.Should().NotBeNull();
                response.Count.Should().Be(2);
            }

            [Fact]
            public void GetStateProvinces_NoStateProvincesFound_ThrowsHttpRequestException()
            {
                //Arrange
                var mockLogger = new Mock<ILogger<ReferenceDataApiClient>>();

                var httpClient = new HttpClient(new HttpMessageHandlerStub(async (request, cancellationToken) =>
                {
                    var responseMessage = new HttpResponseMessage(HttpStatusCode.NotFound);
                    return await Task.FromResult(responseMessage);
                }))
                {
                    BaseAddress = new Uri("http://baseaddress")
                };

                //Act
                var sut = new ReferenceDataApiClient(httpClient, mockLogger.Object);
                Func<Task> func = async () => await sut.GetStateProvincesAsync();

                //Assert
                func.Should().Throw<HttpRequestException>()
                    .WithMessage("Response status code does not indicate success: 404 (Not Found).");
            }
        }

        public class GetTerritories
        {
            [Fact]
            public async void GetTerritories_TerritoriesFound_ReturnsTerritories()
            {
                //Arrange

                var mockLogger = new Mock<ILogger<ReferenceDataApiClient>>();

                var territories = (new string[] { "Northwest", "Northeast" })
                    .Select(country => new SalesTerritoryBuilder()
                     .Name(country)
                     .Build()
                );

                var httpClient = new HttpClient(new HttpMessageHandlerStub(async (request, cancellationToken) =>
                {
                    var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent(
                            JsonSerializer.Serialize(territories, new JsonSerializerOptions
                            {
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
                var sut = new ReferenceDataApiClient(httpClient, mockLogger.Object);
                var response = await sut.GetTerritoriesAsync();

                //Assert
                response.Should().NotBeNull();
                response.Count.Should().Be(2);
            }

            [Fact]
            public void GetTerritories_NoTerritoriesFound_ThrowsHttpRequestException()
            {
                //Arrange
                var mockLogger = new Mock<ILogger<ReferenceDataApiClient>>();

                var httpClient = new HttpClient(new HttpMessageHandlerStub(async (request, cancellationToken) =>
                {
                    var responseMessage = new HttpResponseMessage(HttpStatusCode.NotFound);
                    return await Task.FromResult(responseMessage);
                }))
                {
                    BaseAddress = new Uri("http://baseaddress")
                };

                //Act
                var sut = new ReferenceDataApiClient(httpClient, mockLogger.Object);
                Func<Task> func = async () => await sut.GetTerritoriesAsync();

                //Assert
                func.Should().Throw<HttpRequestException>()
                    .WithMessage("Response status code does not indicate success: 404 (Not Found).");
            }
        }
    }
}