using AW.Application.Interfaces;
using AW.Application.SalesPerson.GetSalesPersons;
using AW.Application.Specifications;
using AW.Application.UnitTests.AutoMapper;
using AW.Application.UnitTests.TestBuilders;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using System.Threading;
using Xunit;

namespace AW.Application.UnitTests
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

            var salesPersonRepoMock = new Mock<IAsyncRepository<Domain.Sales.SalesPerson>>();
            salesPersonRepoMock.Setup(x => x.ListAllAsync())
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

            var salesPersonRepoMock = new Mock<IAsyncRepository<Domain.Sales.SalesPerson>>();
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