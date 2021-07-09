using AW.Services.ReferenceData.Core.Handlers.ContactType.GetContactTypes;
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
    public class ContactTypeControllerUnitTests
    {
        [Fact]
        public async Task GetContactTypes_ContactTypesExists_ReturnContactTypes()
        {
            //Arrange
            var dto = new List<ContactType>
            {
                new ContactType { Name = "Owner" },
                new ContactType { Name = "Order Administrator" }
            };

            var mockLogger = new Mock<ILogger<ContactTypeController>>();
            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(x => x.Send(It.IsAny<GetContactTypesQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(dto);

            var controller = new ContactTypeController(
                mockLogger.Object,
                mockMediator.Object
            );

            //Act
            var actionResult = await controller.GetContactTypes();

            //Assert
            var okObjectResult = actionResult as OkObjectResult;
            okObjectResult.Should().NotBeNull();

            var contactTypes = okObjectResult.Value as List<ContactType>;
            contactTypes.Count.Should().Be(2);
        }
    }
}