using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Admin.Mvc.Services;
using AW.UI.Web.Infrastructure.Api.ReferenceData.Handlers.GetTerritories;
using AW.UI.Web.Infrastructure.Api.SalesOrder.Handlers.GetSalesOrder;
using AW.UI.Web.Infrastructure.Api.SalesOrder.Handlers.GetSalesOrders;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc.Rendering;
using Moq;
using Xunit;

namespace AW.UI.Web.Admin.Mvc.UnitTests.Services;

public class SalesOrderViewModelServiceUnitTests
{
    [Theory, AutoMapperData(typeof(MappingProfile))]
    public async Task GetSalesOrders_ReturnsViewModel(
        [Frozen] Mock<IMediator> mockMediator,
        SalesOrdersResult salesOrdersResult,
        List<Territory> territories,
        SalesOrderService sut
    )
    {
        //Arrange
        salesOrdersResult.TotalSalesOrders = 21;

        mockMediator.Setup(_ => _.Send(
            It.IsAny<GetSalesOrdersQuery>(),
            It.IsAny<CancellationToken>()
        ))
        .ReturnsAsync(salesOrdersResult);

        mockMediator.Setup(_ => _.Send(
                It.IsAny<GetTerritoriesQuery>(),
                It.IsAny<CancellationToken>()
            )
        )
        .ReturnsAsync(territories);

        //Act
        var result = await sut.GetSalesOrders(
            0,
            10,
            string.Empty,
            null
        );

        //Assert
        result.SalesOrders.Count.Should().Be(salesOrdersResult.SalesOrders?.Count);
        result.Territories[0].Should().BeEquivalentTo(new SelectListItem("All", "", true));
        result.Territories.Count.Should().Be(territories.Count + 1);
        result.CustomerTypes[0].Should().BeEquivalentTo(new SelectListItem("All", "", true));
        result.CustomerTypes[1].Should().BeEquivalentTo(new SelectListItem("Individual", "Individual"));
        result.CustomerTypes[2].Should().BeEquivalentTo(new SelectListItem("Store", "Store"));
        result.PaginationInfo.ActualPage.Should().Be(0);
        result.PaginationInfo.ItemsPerPage.Should().Be(salesOrdersResult.SalesOrders?.Count);
        result.PaginationInfo.TotalItems.Should().Be(salesOrdersResult.TotalSalesOrders);
        result.PaginationInfo.TotalPages.Should().Be(3);
        result.PaginationInfo.Previous.Should().Be("disabled");
        result.PaginationInfo.Next.Should().Be("");
    }

    [Theory, AutoMapperData(typeof(MappingProfile))]
    public async Task GetSalesOrderDetail_ReturnsViewModel(
        [Frozen] Mock<IMediator> mockMediator,
        Infrastructure.Api.SalesOrder.Handlers.GetSalesOrder.SalesOrder salesOrder,
        SalesOrderService sut
    )
    {
        //Arrange
        mockMediator.Setup(_ => _.Send(
                It.IsAny<GetSalesOrderQuery>(),
                It.IsAny<CancellationToken>()
            )
        )
        .ReturnsAsync(salesOrder);

        //Act
        var result = await sut.GetSalesOrderDetail("123");

        //Assert
        result.SalesOrder.Should().BeEquivalentTo(salesOrder, opt =>
            opt
                .ExcludingMissingMembers()
                .Excluding(_ => _.RevisionNumber)
                .Excluding(_ => _.Status)
                .Excluding(_ => _.CreditCard)
        );
    }
}
