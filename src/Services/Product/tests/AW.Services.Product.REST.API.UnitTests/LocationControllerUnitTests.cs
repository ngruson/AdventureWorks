using AutoFixture.Xunit2;
using AW.Services.Product.Core.Handlers.GetLocations;
using AW.Services.Product.REST.API.Controllers;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace AW.Services.Product.REST.API.UnitTests
{
    public class LocationControllerUnitTests
    {
        public class GetLocations
        {
            [Theory, AutoMapperData(typeof(MappingProfile), typeof(Core.AutoMapper.MappingProfile))]
            public async Task ReturnLocationsGivenLocationsExist(
                [Frozen] Mock<IMediator> mockMediator,
                List<Location> locations,
                [Greedy] LocationController sut,
                GetLocationsQuery query
            )
            {
                //Arrange
                mockMediator.Setup(x => x.Send(
                    It.IsAny<GetLocationsQuery>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(locations);

                //Act
                var actionResult = await sut.GetLocations(query);

                //Assert
                var okObjectResult = actionResult as OkObjectResult;
                okObjectResult.Should().NotBeNull();

                var response = okObjectResult?.Value as List<Location>;
                response?.Should().BeEquivalentTo(locations);
            }

            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task ReturnNotFoundGivenLocationsDoNotExist(
                [Greedy] LocationController sut,
                GetLocationsQuery query
            )
            {
                //Act
                var actionResult = await sut.GetLocations(query);

                //Assert
                actionResult.Should().BeOfType<NotFoundResult>();
            }
        }
    }
}
