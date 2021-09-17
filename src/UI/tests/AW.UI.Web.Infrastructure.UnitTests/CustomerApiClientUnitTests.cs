using AutoFixture.Xunit2;
using AW.SharedKernel.Interfaces;
using AW.SharedKernel.JsonConverters;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Infrastructure.ApiClients.CustomerApi;
using AW.UI.Web.Infrastructure.ApiClients.CustomerApi.Models.GetCustomers;
using FluentAssertions;
using RichardSzalay.MockHttp;
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

namespace AW.UI.Web.Infrastructure.UnitTests
{
    public class CustomerApiClientUnitTests
    {
        public class GetCustomers
        {
            [Theory, MockHttpData]
            public async Task GetCustomers_CustomersFound_ReturnsCustomer(
                [Frozen] MockHttpMessageHandler handler,
                [Frozen] HttpClient httpClient,
                Uri uri,
                List<StoreCustomer> list,
                CustomerApiClient sut
            )
            {
                //Arrange
                foreach (var item in list)
                {
                    item.CustomerType = SharedKernel.Interfaces.CustomerType.Store;
                }

                var customers = new GetCustomersResponse
                {
                    Customers = list.ToList<Customer>(),
                    TotalCustomers = list.Count
                };

                httpClient.BaseAddress = uri;

                handler.When(HttpMethod.Get, $"{uri}*")
                    .Respond(HttpStatusCode.OK,
                        new StringContent(
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
                    );

                //Act
                var response = await sut.GetCustomersAsync(
                    0, 
                    10, 
                    "territory", 
                    CustomerType.Individual, 
                    "accountNumber"
                );

                //Assert
                response.TotalCustomers.Should().Be(list.Count);
                response.Customers.Should().BeEquivalentTo(list);
            }

            [Theory, MockHttpData]
            public void GetCustomers_NoCustomersFound_ThrowsHttpRequestException(
                [Frozen] MockHttpMessageHandler handler,
                [Frozen] HttpClient httpClient,
                Uri uri,
                CustomerApiClient sut
            )
            {
                //Arrange
                httpClient.BaseAddress = uri;

                handler.When(HttpMethod.Get, $"{uri}*")
                    .Respond(HttpStatusCode.NotFound);

                //Act
                Func<Task> func = async () => await sut.GetCustomersAsync(
                    0, 
                    10, 
                    null, 
                    null, 
                    null
                );

                //Assert
                func.Should().Throw<HttpRequestException>()
                    .WithMessage("Response status code does not indicate success: 404 (Not Found).");
            }
        }

        public class GetCustomer
        {
            [Theory, MockHttpData]
            public async Task GetCustomer_CustomerFound_ReturnCustomer(
                [Frozen] MockHttpMessageHandler handler,
                [Frozen] HttpClient httpClient,
                Uri uri,
                StoreCustomer customer,
                CustomerApiClient sut
            )
            {
                //Arrange
                customer.CustomerType = SharedKernel.Interfaces.CustomerType.Store;
                httpClient.BaseAddress = uri;

                handler.When(HttpMethod.Get, $"{uri}*")
                    .Respond(HttpStatusCode.OK,
                        new StringContent(
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
                    );

                //Act
                var response = await sut.GetCustomerAsync(customer.AccountNumber);

                //Assert
                response.Should().BeEquivalentTo(customer);
            }

            [Theory, MockHttpData]
            public void GetCustomer_CustomerNotFound_ThrowsHttpRequestException(
                [Frozen] MockHttpMessageHandler handler,
                [Frozen] HttpClient httpClient,
                Uri uri,
                CustomerApiClient sut
            )
            {
                //Arrange
                httpClient.BaseAddress = uri;

                handler.When(HttpMethod.Get, $"{uri}*")
                    .Respond(HttpStatusCode.NotFound);

                //Act
                Func<Task> func = async () => await sut.GetCustomerAsync("AW00000001");

                //Assert
                func.Should().Throw<HttpRequestException>()
                    .WithMessage("Response status code does not indicate success: 404 (Not Found).");
            }
        }

        public class UpdateCustomer
        {
            [Theory, MockHttpData]
            public async Task GetCustomer_CustomerFound_ReturnCustomer(
                [Frozen] MockHttpMessageHandler handler,
                [Frozen] HttpClient httpClient,
                Uri uri,
                ApiClients.CustomerApi.Models.UpdateCustomer.StoreCustomer customer,
                CustomerApiClient sut
            )
            {
                //Arrange
                customer.CustomerType = SharedKernel.Interfaces.CustomerType.Store;
                httpClient.BaseAddress = uri;

                handler.When(HttpMethod.Put, $"{uri}*")
                    .Respond(HttpStatusCode.OK,
                        new StringContent(
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
                    );

                //Act
                var response = await sut.UpdateCustomerAsync("1", customer);

                //Assert
                response.Should().BeEquivalentTo(customer);
            }
        }
    }
}