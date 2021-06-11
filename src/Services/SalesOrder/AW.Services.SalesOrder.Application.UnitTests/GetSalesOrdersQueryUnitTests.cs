using Ardalis.Specification;
using AW.Services.SalesOrder.Application.GetSalesOrders;
using AW.Services.SalesOrder.Application.Specifications;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Threading;
using Xunit;

namespace AW.Services.SalesOrder.Application.UnitTests
{
    public class GetSalesOrdersQueryUnitTests
    {
        [Fact]
        public async void Handle_SalesOrdersExists_ReturnSalesOrders()
        {
            var mapper = Mapper.CreateMapper();
            var loggerMock = new Mock<ILogger<GetSalesOrdersQueryHandler>>();
            var salesOrderRepoMock = new Mock<IRepositoryBase<Domain.SalesOrder>>();

            salesOrderRepoMock.Setup(x => x.ListAsync(It.IsAny< GetSalesOrdersPaginatedSpecification>()))
                .ReturnsAsync(new List<Domain.SalesOrder>
                {
                    new TestBuilders.SalesOrderBuilder()
                        .WithTestValues()
                        .Build(),

                    new TestBuilders.SalesOrderBuilder()
                        .SalesOrderNumber("SO43660")
                        .Build()
                });

            salesOrderRepoMock.Setup(x => x.CountAsync(It.IsAny<CountSalesOrdersSpecification>()))
                .ReturnsAsync(2);

            var handler = new GetSalesOrdersQueryHandler(
                loggerMock.Object,
                salesOrderRepoMock.Object,
                mapper
            );

            //Act
            var query = new GetSalesOrdersQuery();
            var result = await handler.Handle(query, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            salesOrderRepoMock.Verify(x => x.ListAsync(It.IsAny< GetSalesOrdersPaginatedSpecification>()));
            result.SalesOrders[0].SalesOrderNumber.Should().Be("SO43659");
            result.SalesOrders[1].SalesOrderNumber.Should().Be("SO43660");
        }
    }
}