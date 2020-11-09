using AW.Application.Interfaces;
using AW.Application.SalesOrder.GetSalesOrders;
using AW.Application.Specifications;
using AW.Application.UnitTests.AutoMapper;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using System.Threading;
using Xunit;

namespace AW.Application.UnitTests
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

            var salesOrderRepoMock = new Mock<IAsyncRepository<Domain.Sales.SalesOrderHeader>>();
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