using Ardalis.Specification;
using AW.Application.SalesTerritory.GetSalesTerritories;
using AW.Application.UnitTests.AutoMapper;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Xunit;

namespace AW.Application.UnitTests
{
    public class GetSalesTerritoriesQueryHandlerUnitTests
    {
        [Fact]
        public async void Handle_ProductsExists_ReturnProducts()
        {
            // Arrange
            var mapper = Mapper.CreateMapper();
            var salesTerritories = new List<Domain.Sales.SalesTerritory>
            {
                new Domain.Sales.SalesTerritory { Name = "Northwest"},
                new Domain.Sales.SalesTerritory { Name = "Northeast"}
            };

            var salesTerritoryRepoMock = new Mock<IRepositoryBase<Domain.Sales.SalesTerritory>>();
            salesTerritoryRepoMock.Setup(x => x.ListAsync())
                .ReturnsAsync(salesTerritories);

            var handler = new GetSalesTerritoriesQueryHandler(
                salesTerritoryRepoMock.Object,
                mapper
            );

            //Act
            var query = new GetSalesTerritoriesQuery();
            var result = await handler.Handle(query, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            result.Count().Should().Be(2);
        }
    }
}