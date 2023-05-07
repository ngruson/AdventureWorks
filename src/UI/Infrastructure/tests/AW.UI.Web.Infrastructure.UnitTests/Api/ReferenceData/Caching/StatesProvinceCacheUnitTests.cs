using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Infrastructure.Api.Interfaces;
using AW.UI.Web.Infrastructure.Api.ReferenceData.Caching;
using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using Xunit;

namespace AW.UI.Web.Infrastructure.UnitTests.Api.ReferenceData.Caching
{
    public class StatesProvinceCacheUnitTests
    {
        public class Initialize
        {
            [Theory, AutoMoqData]
            public async Task Initialize_CacheIsSet(
                [Frozen] Mock<IMemoryCache> cacheMock,
                StatesProvinceCache sut
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
            public async Task GetData_CacheNotSet_StatesProvincesAddedToCache(
                [Frozen] Mock<IMemoryCache> cacheMock,
                [Frozen] Mock<IReferenceDataApiClient> mockClient,
                StatesProvinceCache sut,
                List<Infrastructure.Api.ReferenceData.Handlers.GetStatesProvinces.StateProvince> statesProvinces
            )
            {
                //Arrange
                mockClient.Setup(_ => _.GetStatesProvincesAsync(
                        It.IsAny<string>()
                    )
                )
                .ReturnsAsync(statesProvinces);

                //Act
                var result = await sut.GetData();

                //Assert
                result.Should().BeEquivalentTo(statesProvinces);
                cacheMock.Verify(_ => _.CreateEntry(
                        It.IsAny<string>()
                    )
                );
            }

            [Theory, AutoMoqData]
            public async Task GetData_StatesProvincesAreCached_CacheIsNotSet(
                [Frozen] Mock<IMemoryCache> cacheMock,
                StatesProvinceCache sut,
                List<Infrastructure.Api.ReferenceData.Handlers.GetStatesProvinces.StateProvince> statesProvinces
            )
            {
                //Arrange
                object value = statesProvinces;
                cacheMock.Setup(_ => _.TryGetValue(
                        It.IsAny<object>(),
                        out value!
                    )
                )
                .Returns(true);

                //Act
                var result = await sut.GetData();

                //Assert
                result.Should().BeEquivalentTo(statesProvinces);
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
            public async Task GetData_FilteredStatesProvinces(
                [Frozen] Mock<IReferenceDataApiClient> mockClient,
                StatesProvinceCache sut,
                List<Infrastructure.Api.ReferenceData.Handlers.GetStatesProvinces.StateProvince> statesProvinces
            )
            {
                //Arrange
                mockClient.Setup(_ => _.GetStatesProvincesAsync(
                        It.IsAny<string>()
                    )
                )
                .ReturnsAsync(statesProvinces);

                //Act
                var result = await sut.GetData(_ => _.Name == statesProvinces[0].Name);

                //Assert
                result.Should().BeEquivalentTo(new[] { statesProvinces[0] });
            }
        }
    }
}
