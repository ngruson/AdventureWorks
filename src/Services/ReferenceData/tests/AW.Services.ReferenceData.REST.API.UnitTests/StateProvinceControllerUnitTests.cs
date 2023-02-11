using AutoFixture.Xunit2;
using AW.Services.ReferenceData.Core.Handlers.StateProvince.GetStatesProvinces;
using AW.Services.ReferenceData.REST.API.Controllers;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace AW.Services.ReferenceData.REST.API.UnitTests
{
    public class StateProvinceControllerUnitTests
    {
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task GetStatesProvinces_StatesProvincesExists_ReturnStatesProvinces(
            [Frozen] List<StateProvince> dto,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] StateProvinceController sut
        )
        {
            //Arrange
            mockMediator.Setup(x => x.Send(
                It.IsAny<GetStatesProvincesQuery>(), 
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(dto);

            //Act
            var actionResult = await sut.GetStatesProvinces(dto[0].CountryRegionCode!);

            //Assert
            var okObjectResult = actionResult as OkObjectResult;
            okObjectResult.Should().NotBeNull();

            var statesProvinces = okObjectResult?.Value as List<StateProvince>;
            statesProvinces?.Count.Should().Be(dto.Count);
        }
    }
}