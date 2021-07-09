using AW.SharedKernel.JsonConverters;
using AW.UI.Web.Infrastructure.ApiClients.CustomerApi;
using AW.UI.Web.Infrastructure.ApiClients.CustomerApi.Models.GetCustomers;
using AW.UI.Web.Infrastructure.UnitTests.TestBuilders.GetCustomers;
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
    public class CustomerApiClientUnitTests
    {
        public class GetCustomers
        {
            [Fact]
            public async void GetCustomers_CustomersFound_ReturnsCustomer()
            {
                //Arrange

                var mockLogger = new Mock<ILogger<CustomerApiClient>>();

                var customers = new GetCustomersResponse
                {
                    Customers = new List<Customer>
                    {
                        new StoreCustomerBuilder()
                            .WithTestValues()
                            .Build()
                    },
                    TotalCustomers = 1
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
                                    new JsonStringEnumConverter(),
                                    new CustomerConverter<
                                        Customer,
                                        StoreCustomer,
                                        IndividualCustomer>()
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
                var sut = new CustomerApiClient(httpClient, mockLogger.Object);
                var response = await sut.GetCustomersAsync(0, 10, null, null, null);

                //Assert
                response.Should().NotBeNull();
                response.TotalCustomers.Should().Be(1);
                var customer = response.Customers[0];
                customer.AccountNumber.Should().Be("AW00000001");
                customer.Addresses.Count.Should().Be(1);
                var address = customer.Addresses[0];
                address.AddressType.Should().Be("Main Office");
                address.Address.AddressLine1.Should().Be("2251 Elliot Avenue");
                address.Address.PostalCode.Should().Be("98104");
                address.Address.City.Should().Be("Seattle");
                address.Address.StateProvinceCode.Should().Be("WA");
                address.Address.CountryRegionCode.Should().Be("US");

                (response.Customers[0] as StoreCustomer).Name.Should().Be("A Bike Store");
                (response.Customers[0] as StoreCustomer).SalesPerson.Should().Be("Pamela O Ansman-Wolfe");
                (response.Customers[0] as StoreCustomer).Territory.Should().Be("Northwest");

            }

            [Fact]
            public void GetCustomers_NoCustomersFound_ThrowsHttpRequestException()
            {
                //Arrange

                var mockLogger = new Mock<ILogger<CustomerApiClient>>();

                var httpClient = new HttpClient(new HttpMessageHandlerStub(async (request, cancellationToken) =>
                {
                    var responseMessage = new HttpResponseMessage(HttpStatusCode.NotFound);
                    return await Task.FromResult(responseMessage);
                }))
                {
                    BaseAddress = new Uri("http://baseaddress")
                };

                //Act
                var sut = new CustomerApiClient(httpClient, mockLogger.Object);
                Func<Task> func = async () => await sut.GetCustomersAsync(0, 10, null, null, null);

                //Assert
                func.Should().Throw<HttpRequestException>()
                    .WithMessage("Response status code does not indicate success: 404 (Not Found).");
            }
        }

        public class GetCustomer
        {
            [Fact]
            public async void GetCustomer_CustomerFound_ReturnCustomer()
            {
                //Arrange

                var mockLogger = new Mock<ILogger<CustomerApiClient>>();
                var customer = new TestBuilders.GetCustomer.StoreCustomerBuilder()
                    .WithTestValues()
                    .Build();

                var httpClient = new HttpClient(new HttpMessageHandlerStub(async (request, cancellationToken) =>
                {
                    var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent(
                            JsonSerializer.Serialize(customer, new JsonSerializerOptions
                            {
                                Converters =
                                {
                                new JsonStringEnumConverter(),
                                new CustomerConverter<
                                    Customer,
                                    StoreCustomer,
                                    IndividualCustomer>()
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
                var sut = new CustomerApiClient(httpClient, mockLogger.Object);
                var response = await sut.GetCustomerAsync("AW00000001");

                //Assert
                response.Should().NotBeNull();
                response.AccountNumber.Should().Be(customer.AccountNumber);
                response.Addresses.Should().BeEquivalentTo(customer.Addresses);
                response.SalesOrders.Should().BeEquivalentTo(customer.SalesOrders);

                var store = response as ApiClients.CustomerApi.Models.GetCustomer.StoreCustomer;
                store.Name.Should().Be(customer.Name);
                store.SalesPerson.Should().Be(customer.SalesPerson);
                store.Territory.Should().Be(customer.Territory);
                store.Contacts.Should().BeEquivalentTo(customer.Contacts);
            }

            [Fact]
            public void GetCustomer_CustomerNotFound_ThrowsHttpRequestException()
            {
                //Arrange

                var mockLogger = new Mock<ILogger<CustomerApiClient>>();

                var httpClient = new HttpClient(new HttpMessageHandlerStub(async (request, cancellationToken) =>
                {
                    var responseMessage = new HttpResponseMessage(HttpStatusCode.NotFound);
                    return await Task.FromResult(responseMessage);
                }))
                {
                    BaseAddress = new Uri("http://baseaddress")
                };

                //Act
                var sut = new CustomerApiClient(httpClient, mockLogger.Object);
                Func<Task> func = async () => await sut.GetCustomerAsync("AW00000001");

                //Assert
                func.Should().Throw<HttpRequestException>()
                    .WithMessage("Response status code does not indicate success: 404 (Not Found).");
            }
        }
    }
}