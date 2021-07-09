using AutoMapper;
using AW.Services.ReferenceData.Core.Handlers.Territory.GetTerritories;
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
    public class TerritoryControllerUnitTests
    {
        [Fact]
        public async Task GetTerritories_TerritoriesExists_ReturnTerritories()
        {
            //Arrange
            var dto = new List<Territory>
            {
                new Territory { Name = "Northwest" },
                new Territory { Name = "Northeast" }
            };

            var mockLogger = new Mock<ILogger<TerritoryController>>();
            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(x => x.Send(It.IsAny<GetTerritoriesQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(dto);

            var mapper = new MapperConfiguration(opts => 
                    opts.AddProfile<MappingProfile>())
                .CreateMapper();

            var controller = new TerritoryController(
                mockLogger.Object,
                mockMediator.Object
            );

            //Act
            var actionResult = await controller.GetTerritories();

            //Assert
            var okObjectResult = actionResult as OkObjectResult;
            okObjectResult.Should().NotBeNull();

            var territories = okObjectResult.Value as List<Territory>;
            territories.Count.Should().Be(2);
        }
    }
}