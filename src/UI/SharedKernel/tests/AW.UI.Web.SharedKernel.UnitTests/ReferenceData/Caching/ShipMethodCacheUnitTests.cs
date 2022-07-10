using AutoFixture.Xunit2;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using AW.UI.Web.SharedKernel.ReferenceData.Caching;
using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using Moq;

namespace AW.UI.Web.SharedKernel.UnitTests.ReferenceData.Caching
{
    public class ShipMethodCacheUnitTests
    {
        public class Initialize
        {
            [Theory, AutoMoqData]
            public async Task Initialize_CacheIsSet(
                [Frozen] Mock<IMemoryCache> cacheMock,
                ShipMethodCache sut
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
            public async Task GetData_CacheNotSet_ShipMethodsAddedToCache(
                [Frozen] Mock<IMemoryCache> cacheMock,
                [Frozen] Mock<IReferenceDataApiClient> mockClient,
                ShipMethodCache sut,
                List<SharedKernel.ReferenceData.Handlers.GetShipMethods.ShipMethod> shipMethods
            )
            {
                //Arrange
                mockClient.Setup(_ => _.GetShipMethodsAsync())
                    .ReturnsAsync(shipMethods);

                //Act
                var result = await sut.GetData();

                //Assert
                result.Should().BeEquivalentTo(shipMethods);
                cacheMock.Verify(_ => _.CreateEntry(
                        It.IsAny<string>()
                    )
                );
            }

            [Theory, AutoMoqData]
            public async Task GetData_ShipMethodsAreCached_CacheIsNotSet(
                [Frozen] Mock<IMemoryCache> cacheMock,
                ShipMethodCache sut,
                List<SharedKernel.ReferenceData.Handlers.GetShipMethods.ShipMethod> shipMethods
            )
            {
                //Arrange
                object value = shipMethods;
                cacheMock.Setup(_ => _.TryGetValue(
                        It.IsAny<object>(),
                        out value
                    )
                )
                .Returns(true);

                //Act
                var result = await sut.GetData();

                //Assert
                result.Should().BeEquivalentTo(shipMethods);
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
            public async Task GetData_FilteredShipMethods(
                [Frozen] Mock<IReferenceDataApiClient> mockClient,
                ShipMethodCache sut,
                List<SharedKernel.ReferenceData.Handlers.GetShipMethods.ShipMethod> shipMethods
            )
            {
                //Arrange
                mockClient.Setup(_ => _.GetShipMethodsAsync())
                    .ReturnsAsync(shipMethods);

                //Act
                var result = await sut.GetData(_ => _.Name == shipMethods[0].Name);

                //Assert
                result.Should().BeEquivalentTo(new[] { shipMethods[0] });
            }
        }
    }
}