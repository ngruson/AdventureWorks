﻿using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Admin.Mvc.Services;
using AW.UI.Web.Infrastructure.Api.ReferenceData.Handlers.GetTerritories;
using AW.UI.Web.Infrastructure.Api.SalesPerson.Handlers.GetSalesPersons;
using FluentAssertions;
using MediatR;
using Moq;
using Xunit;

namespace AW.UI.Web.Admin.Mvc.UnitTests.Services;

public class SalesPersonViewModelServiceUnitTests
{
    public class GetSalesPersons
    {
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task GetSalesPersons_ReturnsViewModel(
        [Frozen] Mock<IMediator> mockMediator,
        List<SalesPerson> salesPersons,
        List<Territory> territories,
        SalesPersonViewModelService sut
    )
        {
            //Arrange
            mockMediator.Setup(_ => _.Send(
                    It.IsAny<GetSalesPersonsQuery>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(salesPersons);

            mockMediator.Setup(_ => _.Send(
                    It.IsAny<GetTerritoriesQuery>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(territories);

            //Act
            var result = await sut.GetSalesPersons();

            //Assert
            result.SalesPersons.Should().BeEquivalentTo(salesPersons);
            result.Territories.Should().BeEquivalentTo(territories);
        }
    }
}
