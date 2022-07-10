using AutoFixture.Xunit2;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using AW.UI.Web.SharedKernel.ReferenceData.Caching;
using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using Moq;

namespace AW.UI.Web.SharedKernel.UnitTests.ReferenceData.Caching
{
    public class AddressTypeCacheUnitTests
    {
        public class Initialize
        {
            [Theory, AutoMoqData]
            public async Task Initialize_CacheIsSet(
                [Frozen] Mock<IMemoryCache> cacheMock,
                AddressTypeCache sut
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
            public async Task GetData_CacheNotSet_AddressTypesAddedToCache(
                [Frozen] Mock<IMemoryCache> cacheMock,
                [Frozen] Mock<IReferenceDataApiClient> mockClient,
                AddressTypeCache sut,
                List<SharedKernel.ReferenceData.Handlers.GetAddressTypes.AddressType> addressTypes
            )
            {
                //Arrange
                mockClient.Setup(_ => _.GetAddressTypesAsync())
                    .ReturnsAsync(addressTypes);

                //Act
                var result = await sut.GetData();

                //Assert
                result.Should().BeEquivalentTo(addressTypes);
                cacheMock.Verify(_ => _.CreateEntry(
                        It.IsAny<string>()
                    )
                );
            }

            [Theory, AutoMoqData]
            public async Task GetData_AddressTypesAreCached_CacheIsNotSet(
                [Frozen] Mock<IMemoryCache> cacheMock,
                AddressTypeCache sut,
                List<SharedKernel.ReferenceData.Handlers.GetAddressTypes.AddressType> addressTypes
            )
            {
                //Arrange
                object value = addressTypes;
                cacheMock.Setup(_ => _.TryGetValue(
                        It.IsAny<object>(),
                        out value
                    )
                )
                .Returns(true);

                //Act
                var result = await sut.GetData();

                //Assert
                result.Should().BeEquivalentTo(addressTypes);
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
            public async Task GetData_FilteredAddressTypes(
                [Frozen] Mock<IReferenceDataApiClient> mockClient,
                AddressTypeCache sut,
                List<SharedKernel.ReferenceData.Handlers.GetAddressTypes.AddressType> addressTypes
            )
            {
                //Arrange
                mockClient.Setup(_ => _.GetAddressTypesAsync())
                    .ReturnsAsync(addressTypes);

                //Act
                var result = await sut.GetData(_ => _.Name == addressTypes[0].Name);

                //Assert
                result.Should().BeEquivalentTo(new[] { addressTypes[0] });
            }
        }
    }
}