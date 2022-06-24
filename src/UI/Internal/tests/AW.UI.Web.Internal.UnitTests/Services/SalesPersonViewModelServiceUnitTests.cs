using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Infrastructure.ApiClients.ReferenceDataApi;
using AW.UI.Web.Infrastructure.ApiClients.ReferenceDataApi.Models.GetTerritories;
using AW.UI.Web.Internal.Services;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using AW.UI.Web.SharedKernel.SalesPerson.Handlers.GetSalesPersons;
using FluentAssertions;
using MediatR;
using Moq;
using System.Collections.Generic;
using System.Threading;
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
            [Frozen] Mock<IMediator> mediator,
            [Frozen] Mock<IReferenceDataApiClient> referenceDataApiClient,
            List<SalesPerson> salesPersons,
            List<Territory> territories,
            SalesPersonViewModelService sut
        )
            {
                //Arrange
                mediator.Setup(_ => _.Send(
                    It.IsAny<GetSalesPersonsQuery>(),
                    It.IsAny<CancellationToken>())
                )
                .ReturnsAsync(salesPersons);

                referenceDataApiClient.Setup(x => x.GetTerritoriesAsync(It.IsAny<string>()))
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