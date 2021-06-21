using AutoMapper;
using AW.Services.ReferenceData.Application.CountryRegion.GetCountries;
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
    public class CountryRegionControllerUnitTests
    {
        [Fact]
        public async Task GetCountries_CountriesExists_ReturnCountries()
        {
            //Arrange
            var dto = new List<Country>
            {
                new Country { Name = "United States" },
                new Country { Name = "United Kingdom" }
            };

            var mockLogger = new Mock<ILogger<CountryRegionController>>();
            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(x => x.Send(It.IsAny<GetCountriesQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(dto);

            var mapper = new MapperConfiguration(opts => 
                    opts.AddProfile<MappingProfile>())
                .CreateMapper();

            var controller = new CountryRegionController(
                mockLogger.Object,
                mockMediator.Object,
                mapper
            );

            //Act
            var actionResult = await controller.GetCountries();

            //Assert
            var okObjectResult = actionResult as OkObjectResult;
            okObjectResult.Should().NotBeNull();

            var countries = okObjectResult.Value as List<Country>;
            countries.Count.Should().Be(2);
        }
    }
}