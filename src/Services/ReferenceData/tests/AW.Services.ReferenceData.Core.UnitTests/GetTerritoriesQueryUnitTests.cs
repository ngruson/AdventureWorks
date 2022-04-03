using AutoFixture.Xunit2;
using AW.Services.ReferenceData.Core.Handlers.Territory.GetTerritories;
using AW.Services.ReferenceData.Core.Specifications;
using AW.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.ReferenceData.Core.UnitTests
{
    public class GetTerritoriesQueryUnitTests
    {
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task Handle_WithoutCountryRegionCode_TerritoriesExists_ReturnTerritories(
            List<Entities.Territory> territories,
            [Frozen] Mock<IRepository<Entities.Territory>> territoryRepoMock,
            GetTerritoriesQueryHandler sut,
            GetTerritoriesQuery query
        )
        {
            //Arrange
            territoryRepoMock.Setup(x => x.ListAsync(
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(territories);

            //Act
            query.CountryRegionCode = null;
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            territoryRepoMock.Verify(x => x.ListAsync(
                    It.IsAny<CancellationToken>()
                )
            );

            for (int i = 0; i < result.Count; i++)
            {
                result[i].Name.Should().Be(territories[i].Name);
                result[i].CountryRegionCode.Should().Be(territories[i].CountryRegionCode);
                result[i].Group.Should().Be(territories[i].Group);
            }
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task Handle_WithCountryRegionCode_TerritoriesExists_ReturnTerritories(
            List<Entities.Territory> territories,
            [Frozen] Mock<IRepository<Entities.Territory>> territoryRepoMock,
            GetTerritoriesQueryHandler sut,
            GetTerritoriesQuery query
        )
        {
            //Arrange
            territoryRepoMock.Setup(x => x.ListAsync(
                    It.IsAny<GetTerritoriesForCountrySpecification>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(territories);

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            territoryRepoMock.Verify(x => x.ListAsync(
                    It.IsAny<GetTerritoriesForCountrySpecification>(),
                    It.IsAny<CancellationToken>()
                )
            );

            for (int i = 0; i < result.Count; i++)
            {
                result[i].Name.Should().Be(territories[i].Name);
                result[i].CountryRegionCode.Should().Be(territories[i].CountryRegionCode);
                result[i].Group.Should().Be(territories[i].Group);
            }
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public void Handle_WithoutCountryRegionCode_NoTerritoriesExists_ThrowArgumentNullException(
            [Frozen] Mock<IRepository<Entities.Territory>> territoryRepoMock,
            GetTerritoriesQueryHandler sut,
            GetTerritoriesQuery query
        )
        {
            //Arrange
            territoryRepoMock.Setup(x => x.ListAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync((List<Entities.Territory>)null);

            //Act
            query.CountryRegionCode = null;
            Func<Task> func = async () => await sut.Handle(query, CancellationToken.None);

            //Assert
            func.Should().ThrowAsync<ArgumentNullException>();
            territoryRepoMock.Verify(x => x.ListAsync(
                    It.IsAny<CancellationToken>()
                )
            );
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public void Handle_WithCountryRegionCode_NoTerritoriesExists_ThrowArgumentNullException(
            [Frozen] Mock<IRepository<Entities.Territory>> territoryRepoMock,
            GetTerritoriesQueryHandler sut,
            GetTerritoriesQuery query
        )
        {
            //Arrange
            territoryRepoMock.Setup(x => x.ListAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync((List<Entities.Territory>)null);

            //Act
            Func<Task> func = async () => await sut.Handle(query, CancellationToken.None);

            //Assert
            func.Should().ThrowAsync<ArgumentNullException>();
            territoryRepoMock.Verify(x => x.ListAsync(
                    It.IsAny<GetTerritoriesForCountrySpecification>(),
                    It.IsAny<CancellationToken>()
                )
            );
        }
    }
}