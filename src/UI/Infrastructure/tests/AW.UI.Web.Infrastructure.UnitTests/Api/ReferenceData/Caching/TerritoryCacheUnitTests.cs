﻿using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Infrastructure.Api.Interfaces;
using AW.UI.Web.Infrastructure.Api.ReferenceData.Caching;
using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using Xunit;

namespace AW.UI.Web.Infrastructure.UnitTests.Api.ReferenceData.Caching
{
    public class TerritoryCacheUnitTests
    {
        public class Initialize
        {
            [Theory, AutoMoqData]
            public async Task Initialize_CacheIsSet(
                [Frozen] Mock<IMemoryCache> cacheMock,
                TerritoryCache sut
            )
            {
                //Arrange

                //Act
                await sut.Initialize();

                //Assert
                cacheMock.Verify(_ => _.CreateEntry(
                        It.IsAny<string>()
                    )
                );
            }
        }

        public class GetData
        {
            [Theory, AutoMoqData]
            public async Task GetData_CacheNotSet_StateProvincesAddedToCache(
                [Frozen] Mock<IMemoryCache> cacheMock,
                [Frozen] Mock<IReferenceDataApiClient> mockClient,
                TerritoryCache sut,
                List<Infrastructure.Api.ReferenceData.Handlers.GetTerritories.Territory> territories
            )
            {
                //Arrange
                mockClient.Setup(_ => _.GetTerritoriesAsync(
                        It.IsAny<string>()
                    )
                )
                .ReturnsAsync(territories);

                //Act
                var result = await sut.GetData();

                //Assert
                result.Should().BeEquivalentTo(territories);
                cacheMock.Verify(_ => _.CreateEntry(
                        It.IsAny<string>()
                    )
                );
            }

            [Theory, AutoMoqData]
            public async Task GetData_TerritoriesAreCached_CacheIsNotSet(
                [Frozen] Mock<IMemoryCache> cacheMock,
                TerritoryCache sut,
                List<Infrastructure.Api.ReferenceData.Handlers.GetTerritories.Territory> territories
            )
            {
                //Arrange
                object value = territories;
                cacheMock.Setup(_ => _.TryGetValue(
                        It.IsAny<object>(),
                        out value!
                    )
                )
                .Returns(true);

                //Act
                var result = await sut.GetData();

                //Assert
                result.Should().BeEquivalentTo(territories);
                cacheMock.Verify(_ => _.CreateEntry(
                        It.IsAny<string>()
                    ),
                    Times.Never
                );
            }
        }

        public class GetDataWithPredicate
        {
            [Theory, AutoMoqData]
            public async Task GetData_FilteredTerritories(
                [Frozen] Mock<IReferenceDataApiClient> mockClient,
                TerritoryCache sut,
                List<Infrastructure.Api.ReferenceData.Handlers.GetTerritories.Territory> territories
            )
            {
                //Arrange
                mockClient.Setup(_ => _.GetTerritoriesAsync(
                        It.IsAny<string>()
                    )
                )
                .ReturnsAsync(territories);

                //Act
                var result = await sut.GetData(_ => _.Name == territories[0].Name);

                //Assert
                result.Should().BeEquivalentTo(new[] { territories[0] });
            }
        }
    }
}
