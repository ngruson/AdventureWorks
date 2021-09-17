using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Infrastructure.ApiClients.ReferenceDataApi;
using AW.UI.Web.Infrastructure.ApiClients.ReferenceDataApi.Models.GetTerritories;
using AW.UI.Web.Infrastructure.ApiClients.SalesPersonApi;
using AW.UI.Web.Infrastructure.ApiClients.SalesPersonApi.Models;
using AW.UI.Web.Internal.Services;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace AW.UI.Web.Internal.UnitTests.Services
{
    public class SalesPersonViewModelServiceUnitTests
    {
        public class GetSalesPersons
        {
            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task GetSalesPersons_ReturnsViewModel(
            [Frozen] Mock<ISalesPersonApiClient> salesPersonApiClient,
            [Frozen] Mock<IReferenceDataApiClient> referenceDataApiClient,
            List<SalesPerson> salesPersons,
            List<Territory> territories,
            SalesPersonViewModelService sut
        )
            {
                //Arrange
                salesPersonApiClient.Setup(_ => _.GetSalesPersonsAsync(It.IsAny<string>()))
                    .ReturnsAsync(salesPersons);

                referenceDataApiClient.Setup(x => x.GetTerritoriesAsync())
                    .ReturnsAsync(territories);

                //Act
                var result = await sut.GetSalesPersons();

                //Assert
                result.SalesPersons.Should().BeEquivalentTo(salesPersons);
                result.Territories.Should().BeEquivalentTo(territories);
            }
        }
    }
}