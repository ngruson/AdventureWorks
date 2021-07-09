using AW.Services.ReferenceData.Core.Handlers.CountryRegion.GetCountries;
using AW.SharedKernel.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.ReferenceData.Core.UnitTests
{
    public class GetCountriesQueryUnitTests
    {
        [Fact]
        public async void Handle_CountriesExists_ReturnCountries()
        {
            var mapper = Mapper.CreateMapper();
            var loggerMock = new Mock<ILogger<GetCountriesQueryHandler>>();
            var countryRegionRepoMock = new Mock<IRepository<Entities.CountryRegion>>();

            countryRegionRepoMock.Setup(x => x.ListAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Entities.CountryRegion>
                {
                    new TestBuilders.CountryRegionBuilder()
                        .WithTestValues()
                        .Build(),

                    new TestBuilders.CountryRegionBuilder()
                        .CountryRegionCode("GB")
                        .Name("United Kingdom")
                        .Build()
                });

            var handler = new GetCountriesQueryHandler(
                loggerMock.Object,
                countryRegionRepoMock.Object,
                mapper
            );

            //Act
            var query = new GetCountriesQuery();
            var result = await handler.Handle(query, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            countryRegionRepoMock.Verify(x => x.ListAsync(It.IsAny<CancellationToken>()));
            result[0].CountryRegionCode.Should().Be("US");
            result[1].CountryRegionCode.Should().Be("GB");
        }

        [Fact]
        public void Handle_NoCountriesExists_ThrowException()
        {
            var mapper = Mapper.CreateMapper();
            var loggerMock = new Mock<ILogger<GetCountriesQueryHandler>>();
            var countryRegionRepoMock = new Mock<IRepository<Entities.CountryRegion>>();

            var handler = new GetCountriesQueryHandler(
                loggerMock.Object,
                countryRegionRepoMock.Object,
                mapper
            );

            //Act
            var query = new GetCountriesQuery();
            Func<Task> func = async () => await handler.Handle(query, CancellationToken.None);

            //Assert
            func.Should().Throw<ArgumentNullException>();
            countryRegionRepoMock.Verify(x => x.ListAsync(It.IsAny<CancellationToken>()));
        }
    }
}