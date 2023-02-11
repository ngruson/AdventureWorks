using AutoFixture.Xunit2;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using AW.UI.Web.SharedKernel.SalesPerson.Caching;
using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using Moq;

namespace AW.UI.Web.SharedKernel.UnitTests.SalesPerson.Caching
{
    public class SalesPersonCacheUnitTests
    {
        public class GetData
        {
            [Theory, AutoMoqData]
            public async Task GetData_CacheNotSet_SalesPersonsAddedToCache(
                [Frozen] Mock<IMemoryCache> cacheMock,
                [Frozen] Mock<ISalesPersonApiClient> mockClient,
                SalesPersonCache sut,
                List<SharedKernel.SalesPerson.Handlers.GetSalesPersons.SalesPerson> salesPersons
            )
            {
                //Arrange
                mockClient.Setup(_ => _.GetSalesPersonsAsync(
                        It.IsAny<string>()
                    )
                )
                .ReturnsAsync(salesPersons);
                
                //Act
                var result = await sut.GetData();

                //Assert
                result.Should().BeEquivalentTo(salesPersons);
                cacheMock.Verify(_ => _.CreateEntry(
                        It.IsAny<string>()
                    )
                );
            }

            [Theory, AutoMoqData]
            public async Task GetData_SalesPersonsAreCached_CacheIsNotSet(
                [Frozen] Mock<IMemoryCache> cacheMock,
                SalesPersonCache sut,
                List<SharedKernel.SalesPerson.Handlers.GetSalesPersons.SalesPerson> salesPersons
            )
            {
                //Arrange
                object value = salesPersons;
                cacheMock.Setup(_ => _.TryGetValue(
                        It.IsAny<object>(),
                        out value!
                    )
                )
                .Returns(true);

                //Act
                var result = await sut.GetData();

                //Assert
                result.Should().BeEquivalentTo(salesPersons);
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
            public async Task GetData_FilteredCategories(
                [Frozen] Mock<ISalesPersonApiClient> mockClient,
                SalesPersonCache sut,
                List<SharedKernel.SalesPerson.Handlers.GetSalesPersons.SalesPerson> salesPersons
            )
            {
                //Arrange
                mockClient.Setup(_ => _.GetSalesPersonsAsync(
                        It.IsAny<string>()
                    )
                )
                .ReturnsAsync(salesPersons);

                //Act
                var result = await sut.GetData(_ => _.Name == salesPersons[0].Name);

                //Assert
                result.Should().BeEquivalentTo(new[] { salesPersons[0] });
            }
        }
    }
}
