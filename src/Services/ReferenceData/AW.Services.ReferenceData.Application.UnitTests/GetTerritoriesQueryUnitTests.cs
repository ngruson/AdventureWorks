using Ardalis.Specification;
using AW.Services.ReferenceData.Application.Specifications;
using AW.Services.ReferenceData.Application.StateProvince.GetStatesProvinces;
using AW.Services.ReferenceData.Application.Territory.GetTerritories;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.ReferenceData.Application.UnitTests
{
    public class GetTerritoriesQueryUnitTests
    {
        [Fact]
        public async void Handle_TerritoriesExists_ReturnTerritories()
        {
            var loggerMock = new Mock<ILogger<GetTerritoriesQueryHandler>>();
            var territoryRepoMock = new Mock<IRepositoryBase<Domain.Territory>>();

            territoryRepoMock.Setup(x => x.ListAsync())
                .ReturnsAsync(new List<Domain.Territory>
                {
                    new TestBuilders.TerritoryBuilder()
                        .WithTestValues()
                        .Build(),

                    new TestBuilders.TerritoryBuilder()
                        .Name("Northeast")
                        .CountryRegionCode("US")
                        .Group("North America")
                        .Build()
                });

            var handler = new GetTerritoriesQueryHandler(
                loggerMock.Object,
                territoryRepoMock.Object,
                Mapper.CreateMapper()
            );

            //Act
            var query = new GetTerritoriesQuery();
            var result = await handler.Handle(query, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            territoryRepoMock.Verify(x => x.ListAsync());

            result[0].Name.Should().Be("Northwest");
            result[0].CountryRegionCode.Should().Be("US");
            result[0].Group.Should().Be("North America");

            result[1].Name.Should().Be("Northeast");
            result[1].CountryRegionCode.Should().Be("US");
            result[1].Group.Should().Be("North America");
        }

        [Fact]
        public void Handle_NoTerritoriesExists_ThrowArgumentNullException()
        {
            var loggerMock = new Mock<ILogger<GetTerritoriesQueryHandler>>();
            var territoryRepoMock = new Mock<IRepositoryBase<Domain.Territory>>();

            var handler = new GetTerritoriesQueryHandler(
                loggerMock.Object,
                territoryRepoMock.Object,
                Mapper.CreateMapper()
            );

            //Act
            var query = new GetTerritoriesQuery();
            Func<Task> func = async () => await handler.Handle(query, CancellationToken.None);

            //Assert
            func.Should().Throw<ArgumentNullException>();
            territoryRepoMock.Verify(x => x.ListAsync());
        }
    }
}