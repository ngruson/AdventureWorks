using AutoFixture.Xunit2;
using AW.Services.ReferenceData.Core.Handlers.ContactType.GetContactTypes;
using AW.Services.ReferenceData.REST.API.Controllers;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace AW.Services.ReferenceData.REST.API.UnitTests
{
    public class ContactTypeControllerUnitTests
    {
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task GetContactTypes_ContactTypesExists_ReturnContactTypes(
            [Frozen] List<ContactType> dto,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] ContactTypeController sut
        )
        {
            //Arrange
            mockMediator.Setup(x => x.Send(
                It.IsAny<GetContactTypesQuery>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(dto);

            //Act
            var actionResult = await sut.GetContactTypes();

            //Assert
            var okObjectResult = actionResult as OkObjectResult;
            okObjectResult.Should().NotBeNull();

            var contactTypes = okObjectResult?.Value as List<ContactType>;
            contactTypes?.Count.Should().Be(dto.Count);
        }
    }
}