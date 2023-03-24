using AutoFixture.Xunit2;
using AW.Services.Product.Core.Handlers.GetUnitMeasures;
using AW.Services.Product.REST.API.Controllers;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace AW.Services.Product.REST.API.UnitTests
{
    public class UnitMeasureControllerUnitTests
    {
        public class GetUnitMeasures
        {
            [Theory, AutoMapperData(typeof(MappingProfile), typeof(Core.AutoMapper.MappingProfile))]
            public async Task ReturnUnitMeasuresGivenUnitMeasuresExist(
                [Frozen] Mock<IMediator> mockMediator,
                List<UnitMeasure> unitMeasures,
                [Greedy] UnitMeasureController sut
            )
            {
                //Arrange
                mockMediator.Setup(x => x.Send(
                    It.IsAny<GetUnitMeasuresQuery>(), 
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(unitMeasures);

                //Act
                var actionResult = await sut.GetUnitMeasures();

                //Assert
                var okObjectResult = actionResult as OkObjectResult;
                okObjectResult.Should().NotBeNull();

                var response = okObjectResult?.Value as List<UnitMeasure>;
                response?.Should().BeEquivalentTo(unitMeasures);
            }

            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task ReturnNotFoundGivenUnitMeasuresDoNotExist(
                [Greedy] UnitMeasureController sut
            )
            {
                //Act
                var actionResult = await sut.GetUnitMeasures();

                //Assert
                actionResult.Should().BeOfType<NotFoundResult>();
            }
        }
    }
}
