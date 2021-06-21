using AutoMapper;
using AW.Services.ReferenceData.Application.StateProvince.GetStatesProvinces;
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
    public class StateProvinceControllerUnitTests
    {
        [Fact]
        public async Task GetStatesProvinces_StatesProvincesExists_ReturnStatesProvinces()
        {
            //Arrange
            var dto = new List<StateProvince>
            {
                new StateProvince { Name = "Texas" },
                new StateProvince { Name = "Georgia" }
            };

            var mockLogger = new Mock<ILogger<StateProvinceController>>();
            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(x => x.Send(It.IsAny<GetStatesProvincesQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(dto);

            var mapper = new MapperConfiguration(opts => 
                    opts.AddProfile<MappingProfile>())
                .CreateMapper();

            var controller = new StateProvinceController(
                mockLogger.Object,
                mockMediator.Object
            );

            //Act
            var actionResult = await controller.GetStatesProvinces("US");

            //Assert
            var okObjectResult = actionResult as OkObjectResult;
            okObjectResult.Should().NotBeNull();

            var statesProvinces = okObjectResult.Value as List<StateProvince>;
            statesProvinces.Count.Should().Be(2);
        }
    }
}