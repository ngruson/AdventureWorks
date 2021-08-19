using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Infrastructure.ApiClients.ReferenceDataApi;
using AW.UI.Web.Infrastructure.ApiClients.ReferenceDataApi.Models.GetTerritories;
using AW.UI.Web.Internal.Services;
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
            [Frozen] Mock<IReferenceDataApiClient> referenceDataApiClient,
            List<Territory> territories,
            SalesTerritoryViewModelService sut
        )
        {
            //Arrange
            referenceDataApiClient.Setup(x => x.GetTerritoriesAsync())
                .ReturnsAsync(territories);

            referenceDataApiClient.Setup(x => x.GetTerritoriesAsync())
                .ReturnsAsync(territories);

            //Act
            var result = await sut.GetSalesTerritories();

            //Assert
            result.SalesTerritories.Should().BeEquivalentTo(territories);
        }
    }
}