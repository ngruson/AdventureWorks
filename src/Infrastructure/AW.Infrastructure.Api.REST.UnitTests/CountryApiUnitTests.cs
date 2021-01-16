using AW.Core.Abstractions.Api.CountryApi.ListCountries;
using AW.Infrastructure.Http;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Xunit;

namespace AW.Infrastructure.Api.REST.UnitTests
{
    public class CountryApiUnitTests
    {
        [Fact]
        public async void ListCountries_ReturnsCountries()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CountryApi>>();
            var mockHttpRequestFactory = new Mock<IHttpRequestFactory>();
            mockHttpRequestFactory.Setup(x => x.Get(
                    It.IsAny<string>(), It.IsAny<Dictionary<string, string>>(), It.IsAny<string>()
                )
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new JsonContent(new ListCountriesResponse
                {
                    Countries = new List<Country>
                    {
                        new Country { Name = "United Status" },
                        new Country { Name = "United Kingdom" }
                    }
                })
            });

            var baseAddress = "BaseAddress";

            var sut = new CountryApi(
                mockLogger.Object,
                mockHttpRequestFactory.Object,
                baseAddress
            );

            //Act
            var response = await sut.ListCountriesAsync();

            //Assert
            mockHttpRequestFactory.Verify(x => x.Get(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>(), It.IsAny<string>()));
            response.Countries[0].Name.Should().Be("United Status");
            response.Countries[1].Name.Should().Be("United Kingdom");
        }
    }
}