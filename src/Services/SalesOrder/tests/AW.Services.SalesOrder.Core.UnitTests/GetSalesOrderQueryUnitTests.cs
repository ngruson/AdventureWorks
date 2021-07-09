using Ardalis.Specification;
using AW.Services.SalesOrder.Core.Handlers.GetSalesOrder;
using AW.Services.SalesOrder.Core.Specifications;
using AW.SharedKernel.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading;
using Xunit;

namespace AW.Services.SalesOrder.Core.UnitTests
{
    public class GetSalesOrderQueryUnitTests
    {
        [Fact]
        public async void Handle_SalesOrderExists_ReturnSalesOrder()
        {
            var mapper = Mapper.CreateMapper();
            var loggerMock = new Mock<ILogger<GetSalesOrderQueryHandler>>();
            var salesOrderRepoMock = new Mock<IRepository<Entities.SalesOrder>>();

            salesOrderRepoMock.Setup(x => x.GetBySpecAsync(
                It.IsAny<GetSalesOrderSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(new TestBuilders.SalesOrderBuilder()
                .WithTestValues()
                .Build()
            );

            var handler = new GetSalesOrderQueryHandler(
                loggerMock.Object,
                salesOrderRepoMock.Object,
                mapper
            );

            //Act
            var query = new GetSalesOrderQuery();
            var result = await handler.Handle(query, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            salesOrderRepoMock.Verify(x => x.GetBySpecAsync(
                It.IsAny<GetSalesOrderSpecification>(),
                It.IsAny<CancellationToken>()
            ));
            result.SalesOrderNumber.Should().Be("SO43659");
        }
    }
}