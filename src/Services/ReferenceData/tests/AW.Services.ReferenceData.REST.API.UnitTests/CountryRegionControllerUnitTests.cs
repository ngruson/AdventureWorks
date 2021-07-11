using AutoFixture.Xunit2;
using AW.Services.ReferenceData.Core.Handlers.CountryRegion.GetCountries;
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
    public class CountryRegionControllerUnitTests
    {
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task GetCountries_CountriesExists_ReturnCountries(
            [Frozen] List<Country> dto,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] CountryRegionController sut
        )
        {
            //Arrange
            mockMediator.Setup(x => x.Send(
                It.IsAny<GetCountriesQuery>(), 
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(dto);

            //Act
            var actionResult = await sut.GetCountries();

            //Assert
            var okObjectResult = actionResult as OkObjectResult;
            okObjectResult.Should().NotBeNull();

            var countries = okObjectResult.Value as List<Country>;
            countries.Count.Should().Be(dto.Count);
        }
    }
}