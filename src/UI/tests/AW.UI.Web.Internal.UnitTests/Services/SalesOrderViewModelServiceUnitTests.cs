using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Infrastructure.ApiClients.ReferenceDataApi;
using AW.UI.Web.Infrastructure.ApiClients.ReferenceDataApi.Models.GetTerritories;
using AW.UI.Web.Infrastructure.ApiClients.SalesOrderApi;
using AW.UI.Web.Infrastructure.ApiClients.SalesOrderApi.Models;
using AW.UI.Web.Internal.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace AW.UI.Web.Internal.UnitTests.Services
{
    public class SalesOrderViewModelServiceUnitTests
    {
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task GetSalesOrders_ReturnsViewModel(
            [Frozen] Mock<ISalesOrderApiClient> salesOrderApiClient,
            SalesOrdersResult salesOrdersResult,
            [Frozen] Mock<IReferenceDataApiClient> referenceDataApiClient,
            List<Territory> territories,
            SalesOrderViewModelService sut
        )
        {
            //Arrange
            salesOrdersResult.TotalSalesOrders = 21;

            salesOrderApiClient.Setup(x => x.GetSalesOrdersAsync(
                It.IsAny<int>(), It.IsAny<int>(),
                It.IsAny<string>(), It.IsAny<CustomerType?>()
            ))
            .ReturnsAsync(salesOrdersResult);

            referenceDataApiClient.Setup(x => x.GetTerritoriesAsync())
                .ReturnsAsync(territories);

            //Act
            var result = await sut.GetSalesOrders(
                0,
                10,
                null,
                null
            );

            //Assert
            result.SalesOrders.Count.Should().Be(salesOrdersResult.SalesOrders.Count);
            result.Territories[0].Should().BeEquivalentTo(new SelectListItem("All", "", true));
            result.Territories.Count.Should().Be(territories.Count + 1);
            result.CustomerTypes[0].Should().BeEquivalentTo(new SelectListItem("All", "", true));
            result.CustomerTypes[1].Should().BeEquivalentTo(new SelectListItem("Individual", "Individual"));
            result.CustomerTypes[2].Should().BeEquivalentTo(new SelectListItem("Store", "Store"));
            result.PaginationInfo.ActualPage.Should().Be(0);
            result.PaginationInfo.ItemsPerPage.Should().Be(salesOrdersResult.SalesOrders.Count);
            result.PaginationInfo.TotalItems.Should().Be(salesOrdersResult.TotalSalesOrders);
            result.PaginationInfo.TotalPages.Should().Be(3);
            result.PaginationInfo.Previous.Should().Be("disabled");
            result.PaginationInfo.Next.Should().Be("");
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task GetSalesOrder_ReturnsViewModel(
            [Frozen] Mock<ISalesOrderApiClient> salesOrderApiClient,
            SalesOrder salesOrder,
            SalesOrderViewModelService sut
        )
        {
            //Arrange
            salesOrderApiClient.Setup(x => x.GetSalesOrderAsync(
                It.IsAny<string>()
            ))
            .ReturnsAsync(salesOrder);

            //Act
            var result = await sut.GetSalesOrder("123");

            //Assert
            result.SalesOrder.Should().NotBeNull();
        }
    }
}