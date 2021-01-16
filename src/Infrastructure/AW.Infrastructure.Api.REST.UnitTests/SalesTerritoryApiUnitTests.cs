using AW.Core.Abstractions.Api.SalesTerritoryApi.ListTerritories;
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
    public class SalesTerritoryApiUnitTests
    {
        [Fact]
        public async void ListTerritories_ReturnsTerritories()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<SalesTerritoryApi>>();
            var mockHttpRequestFactory = new Mock<IHttpRequestFactory>();
            mockHttpRequestFactory.Setup(x => x.Get(
                    It.IsAny<string>(), It.IsAny<Dictionary<string, string>>(), It.IsAny<string>()
                )
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new JsonContent(new ListTerritoriesResponse
                {
                    Territories = new List<Territory>
                    {
                        new Territory { Name = "Northwest" },
                        new Territory { Name = "Northeast" }
                    }
                })
            });

            var baseAddress = "BaseAddress";

            var sut = new SalesTerritoryApi(
                mockLogger.Object,
                mockHttpRequestFactory.Object,
                baseAddress
            );

            //Act
            var response = await sut.ListTerritoriesAsync();

            //Assert
            mockHttpRequestFactory.Verify(x => x.Get(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>(), It.IsAny<string>()));
            response.Territories[0].Name.Should().Be("Northwest");
            response.Territories[1].Name.Should().Be("Northeast");
        }
    }
}