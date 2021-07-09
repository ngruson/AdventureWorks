using AW.Services.SalesOrder.Core.Handlers.GetSalesOrdersForCustomer;
using AW.Services.SalesOrder.Core.Specifications;
using AW.SharedKernel.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Threading;
using Xunit;

namespace AW.Services.SalesOrder.Core.UnitTests
{
    public class GetSalesOrdersForCustomerQueryUnitTests
    {
        [Fact]
        public async void Handle_SalesOrderExists_ReturnSalesOrder()
        {
            var mapper = Mapper.CreateMapper();
            var loggerMock = new Mock<ILogger<GetSalesOrdersForCustomerQueryHandler>>();
            var salesOrderRepoMock = new Mock<IRepository<Entities.SalesOrder>>();

            salesOrderRepoMock.Setup(x => x.ListAsync(
                It.IsAny<GetSalesOrdersForCustomerSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(new List<Core.Entities.SalesOrder>
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
            salesOrderRepoMock.Verify(x => x.ListAsync(
                It.IsAny<GetSalesOrdersForCustomerSpecification>(),
                It.IsAny<CancellationToken>()
            ));
            result[0].SalesOrderNumber.Should().Be("SO43659");
            result[1].SalesOrderNumber.Should().Be("SO43660");
        }
    }
}