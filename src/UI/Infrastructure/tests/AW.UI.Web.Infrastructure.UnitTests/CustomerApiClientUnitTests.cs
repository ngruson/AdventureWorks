using AutoFixture.Xunit2;
using AW.SharedKernel.Interfaces;
using AW.SharedKernel.JsonConverters;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using RichardSzalay.MockHttp;
using Microsoft.Extensions.Logging;
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
using GetCusts = AW.UI.Web.SharedKernel.Customer.Handlers.GetCustomers;
using GetCust = AW.UI.Web.SharedKernel.Customer.Handlers.GetCustomer;
using UpdateCust = AW.UI.Web.SharedKernel.Customer.Handlers.UpdateCustomer;
using AW.UI.Web.Infrastructure.ApiClients;
using Moq;

namespace AW.UI.Web.Infrastructure.UnitTests
{
    public class CustomerApiClientUnitTests
    {
        public class GetCustomers
        {
            [Theory, MockHttpData]
            public async Task GetCustomers_StoreCustomersFound_ReturnsStoreCustomer(
                [Frozen] MockHttpMessageHandler handler,
                [Frozen] HttpClient httpClient,
                Uri uri,
                List<GetCusts.StoreCustomer> list,
                CustomerApiClient sut,
                Mock<ILogger<CustomerConverter<GetCusts.Customer, GetCusts.StoreCustomer, GetCusts.IndividualCustomer>>> mockLogger
            )
            {
                //Arrange
                foreach (var item in list)
                {
                    item.CustomerType = CustomerType.Store;
                }

                var customers = new GetCusts.GetCustomersResponse
                {
                    Customers = list.ToList<GetCusts.Customer>(),
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
                                            GetCusts.Customer,
                                            GetCusts.StoreCustomer,
                                            GetCusts.IndividualCustomer>(
                                                mockLogger.Object
                                            )
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
            public async Task GetCustomers_IndividualCustomersFound_ReturnsIndividualCustomer(
                [Frozen] MockHttpMessageHandler handler,
                [Frozen] HttpClient httpClient,
                Uri uri,
                List<GetCusts.IndividualCustomer> list,
                CustomerApiClient sut,
                Mock<ILogger<CustomerConverter<GetCusts.Customer, GetCusts.StoreCustomer, GetCusts.IndividualCustomer>>> mockLogger
            )
            {
                //Arrange
                foreach (var item in list)
                {
                    item.CustomerType = CustomerType.Individual;
                }

                var customers = new GetCusts.GetCustomersResponse
                {
                    Customers = list.ToList<GetCusts.Customer>(),
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
                                            GetCusts.Customer,
                                            GetCusts.StoreCustomer,
                                            GetCusts.IndividualCustomer>(
                                                mockLogger.Object
                                            )
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
            public async Task GetCustomers_NoCustomersFound_ThrowsHttpRequestException(
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
                await func.Should().ThrowAsync<HttpRequestException>()
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
                GetCust.StoreCustomer customer,
                CustomerApiClient sut,
                Mock<ILogger<CustomerConverter<GetCust.Customer, GetCust.StoreCustomer, GetCust.IndividualCustomer>>> mockLogger
            )
            {
                //Arrange
                customer.CustomerType = CustomerType.Store;
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
                                            GetCust.Customer,
                                            GetCust.StoreCustomer,
                                            GetCust.IndividualCustomer>(
                                                mockLogger.Object
                                            )
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
            public async Task GetCustomer_CustomerNotFound_ThrowsHttpRequestException(
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
                await func.Should().ThrowAsync<CustomerApiClientException>()
                    .WithInnerException<CustomerApiClientException, HttpRequestException>()
                    .WithMessage("Response status code does not indicate success: 404 (Not Found).");
            }
        }

        public class GetPreferredAddress
        {
            [Theory, MockHttpData]
            public async Task GetPreferredAddress_AddressFound_ReturnAddress(
                [Frozen] MockHttpMessageHandler handler,
                [Frozen] HttpClient httpClient,
                Uri uri,
                string accountNumber,
                string addressType,
                SharedKernel.Customer.Handlers.GetPreferredAddress.Address address,
                CustomerApiClient sut
            )
            {
                //Arrange
                httpClient.BaseAddress = uri;

                handler.When(HttpMethod.Get, $"{uri}*")
                    .Respond(HttpStatusCode.OK,
                        new StringContent(
                            JsonSerializer.Serialize(address, new JsonSerializerOptions
                            {
                                Converters =
                                    {
                                        new JsonStringEnumConverter()
                                    },

                                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                            }),
                            Encoding.UTF8,
                            "application/json"
                        )
                    );

                //Act
                var response = await sut.GetPreferredAddressAsync(accountNumber, addressType);

                //Assert
                response.Should().BeEquivalentTo(address);
            }

            [Theory, MockHttpData]
            public async Task GetPreferredAddress_CustomerNotFound_ThrowsHttpRequestException(
                [Frozen] MockHttpMessageHandler handler,
                [Frozen] HttpClient httpClient,
                Uri uri,
                string accountNumber,
                string addressType,
                CustomerApiClient sut
            )
            {
                //Arrange
                httpClient.BaseAddress = uri;

                handler.When(HttpMethod.Get, $"{uri}*")
                    .Respond(HttpStatusCode.NotFound);

                //Act
                Func<Task> func = async () => await sut.GetPreferredAddressAsync(accountNumber, addressType);

                //Assert
                await func.Should().ThrowAsync<CustomerApiClientException>()
                    .WithInnerException<CustomerApiClientException, HttpRequestException>()
                    .WithMessage("Response status code does not indicate success: 404 (Not Found).");
            }
        }

        public class UpdateCustomer
        {
            [Theory, MockHttpData]
            public async Task UpdateCustomer_StoreCustomerFound_ReturnStoreCustomer(
                [Frozen] MockHttpMessageHandler handler,
                [Frozen] HttpClient httpClient,
                Uri uri,
                UpdateCust.StoreCustomer customer,
                CustomerApiClient sut,
                Mock<ILogger<CustomerConverter<UpdateCust.Customer, UpdateCust.StoreCustomer, UpdateCust.IndividualCustomer>>> mockLogger
            )
            {
                //Arrange
                customer.CustomerType = CustomerType.Store;
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
                                            UpdateCust.Customer,
                                            UpdateCust.StoreCustomer,
                                            UpdateCust.IndividualCustomer>(
                                                mockLogger.Object
                                            )
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

            [Theory, MockHttpData]
            public async Task UpdateCustomer_IndividualCustomerFound_ReturnIndividualCustomer(
                [Frozen] MockHttpMessageHandler handler,
                [Frozen] HttpClient httpClient,
                Uri uri,
                UpdateCust.IndividualCustomer customer,
                CustomerApiClient sut,
                Mock<ILogger<CustomerConverter<UpdateCust.Customer, UpdateCust.StoreCustomer, UpdateCust.IndividualCustomer>>> mockLogger
            )
            {
                //Arrange
                customer.CustomerType = CustomerType.Individual;
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
                                            UpdateCust.Customer,
                                            UpdateCust.StoreCustomer,
                                            UpdateCust.IndividualCustomer>(
                                                mockLogger.Object
                                            )
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