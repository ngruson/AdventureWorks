using AutoFixture.Xunit2;
using AW.SharedKernel.Caching;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Internal.Services;
using AW.UI.Web.SharedKernel.ReferenceData.Handlers.GetTerritories;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace AW.UI.Web.Internal.UnitTests.Services
{
    public class SalesTerritoryViewModelServiceUnitTests
    {
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task GetSalesOrders_ReturnsViewModel(
            [Frozen] Mock<ICache<Territory>> mockCache,
            List<Territory> territories,
            SalesTerritoryViewModelService sut
        )
        {
            //Arrange
            mockCache.Setup(x => x.GetData())
                .ReturnsAsync(territories);

            //Act
            var result = await sut.GetSalesTerritories();

            //Assert
            result.SalesTerritories.Should().BeEquivalentTo(territories);
        }
    }
}