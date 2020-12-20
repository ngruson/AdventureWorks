using Ardalis.Specification;
using AW.Core.Application.SalesPerson.GetSalesPersons;
using AW.Core.Application.Specifications;
using AW.Core.Application.UnitTests.AutoMapper;
using AW.Core.Application.UnitTests.TestBuilders;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using System.Threading;
using Xunit;

namespace AW.Core.Application.UnitTests
{
    public class GetSalesPersonsQueryHandlerUnitTests
    {
        [Fact]
        public async void Handle_SalesPersonsExists_ReturnSalesPersons()
        {
            // Arrange
            var mapper = Mapper.CreateMapper();
            var salesPersons = new List<Domain.Sales.SalesPerson>
            {
                new SalesPersonBuilder().WithTestValues().Build()
            };

            var salesPersonRepoMock = new Mock<IRepositoryBase<Domain.Sales.SalesPerson>>();
            salesPersonRepoMock.Setup(x => x.ListAsync())
                .ReturnsAsync(salesPersons);

            var handler = new GetSalesPersonsQueryHandler(
                salesPersonRepoMock.Object,
                mapper
            );

            //Act
            var query = new GetSalesPersonsQuery();
            var result = await handler.Handle(query, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async void Handle_FilterByTerritory_ReturnSalesPersons()
        {
            // Arrange
            var mapper = Mapper.CreateMapper();
            var salesPersons = new List<Domain.Sales.SalesPerson>
            {
                new SalesPersonBuilder().WithTestValues().Build()
            };

            var salesPersonRepoMock = new Mock<IRepositoryBase<Domain.Sales.SalesPerson>>();
            salesPersonRepoMock.Setup(x => x.ListAsync(It.IsAny<GetSalesPersonsSpecification>()))
                .ReturnsAsync(salesPersons);

            var handler = new GetSalesPersonsQueryHandler(
                salesPersonRepoMock.Object,
                mapper
            );

            //Act
            var query = new GetSalesPersonsQuery
            {
                SalesTerritoryName = "Northwest"
            };
            var result = await handler.Handle(query, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
        }
    }
}