﻿using AutoFixture.Xunit2;
using AW.SharedKernel.Interfaces;
using AW.SharedKernel.JsonConverters;
using AW.SharedKernel.UnitTesting;
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
using AW.UI.Web.SharedKernel.Customer.Handlers.GetCustomers;
using AW.UI.Web.SharedKernel.Customer.Handlers.UpdateCustomer;
using AW.UI.Web.Infrastructure.ApiClients;

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
                List<SharedKernel.Customer.Handlers.GetCustomers.StoreCustomer> list,
                CustomerApiClient sut
            )
            {
                //Arrange
                foreach (var item in list)
                {
                    item.CustomerType = CustomerType.Store;
                }

                var customers = new GetCustomersResponse
                {
                    Customers = list.ToList<SharedKernel.Customer.Handlers.GetCustomers.Customer>(),
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
                                            SharedKernel.Customer.Handlers.GetCustomers.Customer,
                                            SharedKernel.Customer.Handlers.GetCustomers.StoreCustomer,
                                            SharedKernel.Customer.Handlers.GetCustomers.IndividualCustomer>()
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
                List<SharedKernel.Customer.Handlers.GetCustomers.IndividualCustomer> list,
                CustomerApiClient sut
            )
            {
                //Arrange
                foreach (var item in list)
                {
                    item.CustomerType = CustomerType.Individual;
                }

                var customers = new GetCustomersResponse
                {
                    Customers = list.ToList<SharedKernel.Customer.Handlers.GetCustomers.Customer>(),
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
                                            SharedKernel.Customer.Handlers.GetCustomers.Customer,
                                            SharedKernel.Customer.Handlers.GetCustomers.StoreCustomer,
                                            SharedKernel.Customer.Handlers.GetCustomers.IndividualCustomer>()
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
                SharedKernel.Customer.Handlers.GetCustomer.StoreCustomer customer,
                CustomerApiClient sut
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
                                            SharedKernel.Customer.Handlers.GetCustomer.Customer,
                                            SharedKernel.Customer.Handlers.GetCustomer.StoreCustomer,
                                            SharedKernel.Customer.Handlers.GetCustomer.IndividualCustomer>()
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
                SharedKernel.Customer.Handlers.UpdateCustomer.StoreCustomer customer,
                CustomerApiClient sut
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
                                            SharedKernel.Customer.Handlers.UpdateCustomer.Customer,
                                            SharedKernel.Customer.Handlers.UpdateCustomer.StoreCustomer,
                                            SharedKernel.Customer.Handlers.UpdateCustomer.IndividualCustomer>()
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
                SharedKernel.Customer.Handlers.UpdateCustomer.IndividualCustomer customer,
                CustomerApiClient sut
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
                                            SharedKernel.Customer.Handlers.UpdateCustomer.Customer,
                                            SharedKernel.Customer.Handlers.UpdateCustomer.StoreCustomer,
                                            SharedKernel.Customer.Handlers.UpdateCustomer.IndividualCustomer>()
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