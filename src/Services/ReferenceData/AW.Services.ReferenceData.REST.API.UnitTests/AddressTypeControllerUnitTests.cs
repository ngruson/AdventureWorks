using AW.Services.ReferenceData.Application.AddressType.GetAddressTypes;
using AW.Services.ReferenceData.REST.API.Controllers;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.ReferenceData.REST.API.UnitTests
{
    public class AddressTypeControllerUnitTests
    {
        [Fact]
        public async Task GetAddressTypes_AddressTypesExists_ReturnAddressTypes()
        {
            //Arrange
            var dto = new List<AddressType>
            {
                new AddressType { Name = "Home" },
                new AddressType { Name = "Main Office" }
            };

            var mockLogger = new Mock<ILogger<AddressTypeController>>();
            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(x => x.Send(It.IsAny<GetAddressTypesQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(dto);

            var controller = new AddressTypeController(
                mockLogger.Object,
                mockMediator.Object
            );

            //Act
            var actionResult = await controller.GetAddressTypes();

            //Assert
            var okObjectResult = actionResult as OkObjectResult;
            okObjectResult.Should().NotBeNull();

            var adressTypes = okObjectResult.Value as List<AddressType>;
            adressTypes.Count.Should().Be(2);
        }
    }
}