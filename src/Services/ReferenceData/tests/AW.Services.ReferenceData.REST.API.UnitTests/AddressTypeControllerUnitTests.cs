using AutoFixture.Xunit2;
using AW.Services.ReferenceData.Core.Handlers.AddressType.GetAddressTypes;
using AW.Services.ReferenceData.REST.API.Controllers;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.ReferenceData.REST.API.UnitTests
{
    public class AddressTypeControllerUnitTests
    {
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task GetAddressTypes_AddressTypesExists_ReturnAddressTypes(
            [Frozen] List<AddressType> dto,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] AddressTypeController sut
        )
        {
            //Arrange
            mockMediator.Setup(x => x.Send(
                It.IsAny<GetAddressTypesQuery>(), 
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(dto);

            //Act
            var actionResult = await sut.GetAddressTypes();

            //Assert
            var okObjectResult = actionResult as OkObjectResult;
            okObjectResult.Should().NotBeNull();

            var adressTypes = okObjectResult.Value as List<AddressType>;
            adressTypes.Count.Should().Be(dto.Count);
        }
    }
}