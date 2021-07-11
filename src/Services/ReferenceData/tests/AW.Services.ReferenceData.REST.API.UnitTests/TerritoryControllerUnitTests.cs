using AutoFixture.Xunit2;
using AW.Services.ReferenceData.Core.Handlers.Territory.GetTerritories;
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
    public class TerritoryControllerUnitTests
    {
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task GetTerritories_TerritoriesExists_ReturnTerritories(
            [Frozen] List<Territory> dto,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] TerritoryController sut
        )
        {
            //Arrange
            mockMediator.Setup(x => x.Send(
                It.IsAny<GetTerritoriesQuery>(), 
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(dto);

            //Act
            var actionResult = await sut.GetTerritories();

            //Assert
            var okObjectResult = actionResult as OkObjectResult;
            okObjectResult.Should().NotBeNull();

            var territories = okObjectResult.Value as List<Territory>;
            territories.Count.Should().Be(dto.Count);
        }
    }
}