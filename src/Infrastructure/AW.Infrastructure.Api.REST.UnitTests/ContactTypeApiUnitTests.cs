using AW.Core.Abstractions.Api.ContactTypeApi.ListContactTypes;
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
    public class ContactTypeApiUnitTests
    {
        [Fact]
        public async void ListContactTypes_ReturnsContactTypes()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<ContactTypeApi>>();
            var mockHttpRequestFactory = new Mock<IHttpRequestFactory>();
            mockHttpRequestFactory.Setup(x => x.Get(
                    It.IsAny<string>(), It.IsAny<Dictionary<string, string>>(), It.IsAny<string>()
                )
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new JsonContent(new ListContactTypesResponse
                {
                    ContactTypes = new List<string>
                    {
                        "Owner", "Order Administrator"
                    }
                })
            });

            var baseAddress = "BaseAddress";

            var sut = new ContactTypeApi(
                mockLogger.Object,
                mockHttpRequestFactory.Object,
                baseAddress
            );

            //Act
            var response = await sut.ListContactTypesAsync();

            //Assert
            mockHttpRequestFactory.Verify(x => x.Get(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>(), It.IsAny<string>()));
            response.ContactTypes[0].Should().Be("Owner");
            response.ContactTypes[1].Should().Be("Order Administrator");
        }
    }
}