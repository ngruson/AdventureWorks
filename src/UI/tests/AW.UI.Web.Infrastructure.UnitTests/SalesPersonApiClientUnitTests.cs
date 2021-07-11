﻿using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Infrastructure.ApiClients.SalesPersonApi;
using AW.UI.Web.Infrastructure.ApiClients.SalesPersonApi.Models;
using FluentAssertions;
using RichardSzalay.MockHttp;
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
            [Theory, MockHttpData]
            public async void GetSalesPersons_SalesPersonsFound_ReturnsSalesPersons(
                [Frozen] MockHttpMessageHandler handler,
                [Frozen] HttpClient httpClient,
                Uri uri,
                List<SalesPerson> salesPersons,
                SalesPersonApiClient sut
            )
            {
                //Arrange
                httpClient.BaseAddress = uri;

                handler.When(HttpMethod.Get, $"{uri}*")
                    .Respond(HttpStatusCode.OK,
                        new StringContent(
                            JsonSerializer.Serialize(
                                salesPersons,
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
                var response = await sut.GetSalesPersonsAsync();

                //Assert
                response.Should().NotBeNull();
                response.Should().BeEquivalentTo(salesPersons);
            }

            [Theory, MockHttpData]
            public void GetSalesPersons_NoSalesPersonsFound_ThrowsHttpRequestException(
                [Frozen] MockHttpMessageHandler handler,
                [Frozen] HttpClient httpClient,
                Uri uri,
                SalesPersonApiClient sut
            )
            {
                //Arrange
                httpClient.BaseAddress = uri;

                handler.When(HttpMethod.Get, $"{uri}*")
                    .Respond(HttpStatusCode.NotFound);

                //Act
                Func<Task> func = async () => await sut.GetSalesPersonsAsync();

                //Assert
                func.Should().Throw<HttpRequestException>()
                    .WithMessage("Response status code does not indicate success: 404 (Not Found).");
            }
        }

        public class GetSalesPerson
        {
            [Theory, MockHttpData]
            public async void GetSalesPerson_SalesPersonFound_ReturnsSalesPerson(
                [Frozen] MockHttpMessageHandler handler,
                [Frozen] HttpClient httpClient,
                Uri uri,
                SalesPerson salesPerson,
                SalesPersonApiClient sut
            )
            {
                //Arrange
                httpClient.BaseAddress = uri;

                handler.When(HttpMethod.Get, $"{uri}*")
                    .Respond(HttpStatusCode.OK,
                        new StringContent(
                            JsonSerializer.Serialize(
                                salesPerson,
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
                var response = await sut.GetSalesPersonAsync(
                    salesPerson.FirstName,
                    salesPerson.MiddleName,
                    salesPerson.LastName
                );

                //Assert
                response.Should().BeEquivalentTo(salesPerson);
            }

            [Theory, MockHttpData]
            public void GetSalesPerson_NoSalesPersonFound_ThrowsHttpRequestException(
                [Frozen] MockHttpMessageHandler handler,
                [Frozen] HttpClient httpClient,
                Uri uri,
                SalesPersonApiClient sut
            )
            {
                //Arrange
                httpClient.BaseAddress = uri;

                handler.When(HttpMethod.Get, $"{uri}*")
                    .Respond(HttpStatusCode.NotFound);

                //Act
                Func<Task> func = async () => await sut.GetSalesPersonAsync("", "", "");

                //Assert
                func.Should().Throw<HttpRequestException>()
                    .WithMessage("Response status code does not indicate success: 404 (Not Found).");
            }
        }
    }
}