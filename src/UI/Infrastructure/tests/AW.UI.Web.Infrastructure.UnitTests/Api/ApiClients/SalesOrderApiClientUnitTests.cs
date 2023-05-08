﻿using AutoFixture.Xunit2;
using AW.SharedKernel.JsonConverters;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using RichardSzalay.MockHttp;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Xunit;
using AW.UI.Web.Infrastructure.Api.ApiClients;
using AW.UI.Web.Infrastructure.Api.SalesOrder.Handlers.GetSalesOrders;
using AW.UI.Web.Infrastructure.Api.SalesOrder.Handlers.GetSalesOrder;

namespace AW.UI.Web.Infrastructure.UnitTests.Api.ApiClients
{
    public class SalesOrderApiClientUnitTests
    {
        public class GetSalesOrders
        {
            [Theory, MockHttpData]
            public async Task GetSalesOrders_SalesOrdersFound_ReturnsSalesOrders(
                [Frozen] MockHttpMessageHandler handler,
                [Frozen] HttpClient httpClient,
                Mock<ILogger<CustomerConverter<Infrastructure.Api.SalesOrder.Handlers.GetSalesOrders.Customer,
                    Infrastructure.Api.SalesOrder.Handlers.GetSalesOrders.StoreCustomer,
                    Infrastructure.Api.SalesOrder.Handlers.GetSalesOrders.IndividualCustomer>>> mockLogger,
                Uri uri,
                List<Infrastructure.Api.SalesOrder.Handlers.GetSalesOrders.SalesOrder> salesOrders,
                Infrastructure.Api.SalesOrder.Handlers.GetSalesOrders.IndividualCustomer customer,
                SalesOrderApiClient sut,
                int pageIndex,
                int pageSize,
                string territory,
                Infrastructure.Api.SalesOrder.Handlers.GetSalesOrders.CustomerType customerType
            )
            {
                //Arrange
                httpClient.BaseAddress = uri;
                salesOrders.ForEach(_ => _.Customer = customer);

                var salesOrdersResult = new Infrastructure.Api.SalesOrder.Handlers.GetSalesOrders.SalesOrdersResult
                {
                    SalesOrders = salesOrders,
                    TotalSalesOrders = salesOrders.Count
                };

                handler.When(HttpMethod.Get, $"{uri}*")
                    .Respond(HttpStatusCode.OK,
                        new StringContent(
                            JsonSerializer.Serialize(
                                salesOrdersResult,
                                new JsonSerializerOptions
                                {
                                    Converters =
                                    {
                                        new JsonStringEnumConverter(),
                                        new CustomerConverter<
                                            Infrastructure.Api.SalesOrder.Handlers.GetSalesOrders.Customer,
                                            Infrastructure.Api.SalesOrder.Handlers.GetSalesOrders.StoreCustomer,
                                            Infrastructure.Api.SalesOrder.Handlers.GetSalesOrders.IndividualCustomer>(
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
                var response = await sut.GetSalesOrdersAsync(pageIndex, pageSize, territory, customerType);

                //Assert
                response.Should().BeEquivalentTo(salesOrdersResult);
            }

            [Theory, MockHttpData]
            public async Task GetSalesOrders_NoSalesOrdersFound_ThrowsHttpRequestException(
                [Frozen] MockHttpMessageHandler handler,
                [Frozen] HttpClient httpClient,
                Uri uri,
                SalesOrderApiClient sut
            )
            {
                //Arrange
                httpClient.BaseAddress = uri;

                handler.When(HttpMethod.Get, $"{uri}*")
                    .Respond(HttpStatusCode.NotFound);

                //Act
                Func<Task> func = async () => await sut.GetSalesOrdersAsync(0, 10, null, null);

                //Assert
                await func.Should().ThrowAsync<HttpRequestException>()
                    .WithMessage("Response status code does not indicate success: 404 (Not Found).");
            }
        }

        public class GetSalesOrder
        {
            [Theory, MockHttpData]
            public async Task GetSalesOrder_SalesOrderFound_ReturnsSalesOrder(
                [Frozen] MockHttpMessageHandler handler,
                [Frozen] HttpClient httpClient,
                Mock<ILogger<CustomerConverter<Infrastructure.Api.SalesOrder.Handlers.GetSalesOrder.Customer, 
                    Infrastructure.Api.SalesOrder.Handlers.GetSalesOrder.StoreCustomer, 
                    Infrastructure.Api.SalesOrder.Handlers.GetSalesOrder.IndividualCustomer>>> mockLogger,
                Uri uri,
                Infrastructure.Api.SalesOrder.Handlers.GetSalesOrder.SalesOrder salesOrder,
                Infrastructure.Api.SalesOrder.Handlers.GetSalesOrder.IndividualCustomer customer,
                SalesOrderApiClient sut
            )
            {
                //Arrange
                httpClient.BaseAddress = uri;
                salesOrder.Customer = customer;

                handler.When(HttpMethod.Get, $"{uri}*")
                    .Respond(HttpStatusCode.OK,
                        new StringContent(
                            JsonSerializer.Serialize(
                                salesOrder,
                                new JsonSerializerOptions
                                {
                                    Converters =
                                    {
                                        new JsonStringEnumConverter(),
                                        new CustomerConverter<
                                            Infrastructure.Api.SalesOrder.Handlers.GetSalesOrder.Customer,
                                            Infrastructure.Api.SalesOrder.Handlers.GetSalesOrder.StoreCustomer,
                                            Infrastructure.Api.SalesOrder.Handlers.GetSalesOrder.IndividualCustomer>(
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
                var response = await sut.GetSalesOrderAsync(salesOrder.SalesOrderNumber);

                //Assert
                response.Should().BeEquivalentTo(salesOrder);
            }

            [Theory, MockHttpData]
            public async Task GetSalesOrder_NoSalesOrderFound_ThrowsHttpRequestException(
                [Frozen] MockHttpMessageHandler handler,
                [Frozen] HttpClient httpClient,
                Uri uri,
                SalesOrderApiClient sut
            )
            {
                //Arrange
                httpClient.BaseAddress = uri;

                handler.When(HttpMethod.Get, $"{uri}*")
                    .Respond(HttpStatusCode.NotFound);

                //Act
                Func<Task> func = async () => await sut.GetSalesOrderAsync("123");

                //Assert
                await func.Should().ThrowAsync<HttpRequestException>()
                    .WithMessage("Response status code does not indicate success: 404 (Not Found).");
            }
        }
    }
}