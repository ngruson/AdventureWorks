using Ardalis.Specification;
using AW.Application.SalesPerson.GetSalesPerson;
using AW.Application.UnitTests.AutoMapper;
using AW.Application.UnitTests.TestBuilders;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using System.Threading;
using Xunit;

namespace AW.Application.UnitTests
{
    public class GetSalesPersonQueryHandlerUnitTests
    {
        [Fact]
        public async void Handle_SalesPersonExists_ReturnSalesPerson()
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

            var handler = new GetSalesPersonQueryHandler(
                salesPersonRepoMock.Object,
                mapper
            );

            //Act
            var query = new GetSalesPersonQuery
            {  
                FullName = salesPersons[0].FullName
            };
            var result = await handler.Handle(query, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
        }
    }
}