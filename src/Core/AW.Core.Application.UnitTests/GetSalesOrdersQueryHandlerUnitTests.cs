using Ardalis.Specification;
using AW.Core.Application.SalesOrder.GetSalesOrders;
using AW.Core.Application.Specifications;
using AW.Core.Application.UnitTests.AutoMapper;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using System.Threading;
using Xunit;

namespace AW.Core.Application.UnitTests
{
    public class GetSalesOrdersQueryHandlerUnitTests
    {
        [Fact]
        public async void Handle_SalesOrdersExists_ReturnSalesOrders()
        {
            // Arrange
            var mapper = Mapper.CreateMapper();
            var salesOrders = new List<Domain.Sales.SalesOrderHeader>
            {
                new Domain.Sales.SalesOrderHeader { SalesOrderNumber = "SO43659" },
                new Domain.Sales.SalesOrderHeader { SalesOrderNumber = "SO43660" }
            };

            var salesOrderRepoMock = new Mock<IRepositoryBase<Domain.Sales.SalesOrderHeader>>();
            salesOrderRepoMock.Setup(x => x.ListAsync(It.IsAny<GetSalesOrdersPaginatedSpecification>()))
                .ReturnsAsync(salesOrders);
            salesOrderRepoMock.Setup(x => x.CountAsync(It.IsAny<CountSalesOrdersSpecification>()))
                .ReturnsAsync(2);

            var handler = new GetSalesOrdersQueryHandler(
                salesOrderRepoMock.Object,
                mapper
            );

            //Act
            var query = new GetSalesOrdersQuery();
            var result = await handler.Handle(query, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
        }
    }
}