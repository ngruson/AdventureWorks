using AutoFixture.Xunit2;
using AW.Services.ReferenceData.Core.Handlers.ShipMethod.GetShipMethods;
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
    public class ShipMethodControllerUnitTests
    {
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task GetShipMethods_ShipMethodsExists_ReturnShipMethods(
            [Frozen] List<ShipMethod> dto,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] ShipMethodController sut
        )
        {
            //Arrange
            mockMediator.Setup(x => x.Send(
                It.IsAny<GetShipMethodsQuery>(), 
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(dto);

            //Act
            var actionResult = await sut.GetShipMethods();

            //Assert
            var okObjectResult = actionResult as OkObjectResult;
            okObjectResult.Should().NotBeNull();

            var shipMethods = okObjectResult.Value as List<ShipMethod>;
            shipMethods.Count.Should().Be(dto.Count);
        }
    }
}