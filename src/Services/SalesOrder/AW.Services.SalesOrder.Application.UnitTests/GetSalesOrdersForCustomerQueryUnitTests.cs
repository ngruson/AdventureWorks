using Ardalis.Specification;
using AW.Services.SalesOrder.Application.GetSalesOrdersForCustomer;
using AW.Services.SalesOrder.Application.Specifications;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Threading;
using Xunit;

namespace AW.Services.SalesOrder.Application.UnitTests
{
    public class GetSalesOrdersForCustomerQueryUnitTests
    {
        [Fact]
        public async void Handle_SalesOrderExists_ReturnSalesOrder()
        {
            var mapper = Mapper.CreateMapper();
            var loggerMock = new Mock<ILogger<GetSalesOrdersForCustomerQueryHandler>>();
            var salesOrderRepoMock = new Mock<IRepositoryBase<Domain.SalesOrder>>();

            salesOrderRepoMock.Setup(x => x.ListAsync(It.IsAny<GetSalesOrdersForCustomerSpecification>()))
                .ReturnsAsync(new List<Domain.SalesOrder>
                {
                    new TestBuilders.SalesOrderBuilder()
                        .WithTestValues()
                        .Build(),

                    new TestBuilders.SalesOrderBuilder()
                        .SalesOrderNumber("SO43660")
                        .Build()
                });

            var handler = new GetSalesOrdersForCustomerQueryHandler(
                loggerMock.Object,
                salesOrderRepoMock.Object,
                mapper
            );

            //Act
            var query = new GetSalesOrdersForCustomerQuery();
            var result = await handler.Handle(query, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            salesOrderRepoMock.Verify(x => x.ListAsync(It.IsAny<GetSalesOrdersForCustomerSpecification>()));
            result[0].SalesOrderNumber.Should().Be("SO43659");
            result[1].SalesOrderNumber.Should().Be("SO43660");
        }
    }
}