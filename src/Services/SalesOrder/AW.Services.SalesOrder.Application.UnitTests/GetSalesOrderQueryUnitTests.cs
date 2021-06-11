using Ardalis.Specification;
using AW.Services.SalesOrder.Application.GetSalesOrder;
using AW.Services.SalesOrder.Application.Specifications;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading;
using Xunit;

namespace AW.Services.SalesOrder.Application.UnitTests
{
    public class GetSalesOrderQueryUnitTests
    {
        [Fact]
        public async void Handle_SalesOrderExists_ReturnSalesOrder()
        {
            var mapper = Mapper.CreateMapper();
            var loggerMock = new Mock<ILogger<GetSalesOrderQueryHandler>>();
            var salesOrderRepoMock = new Mock<IRepositoryBase<Domain.SalesOrder>>();

            salesOrderRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetSalesOrderSpecification>()))
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
            salesOrderRepoMock.Verify(x => x.GetBySpecAsync(It.IsAny<GetSalesOrderSpecification>()));
            result.SalesOrderNumber.Should().Be("SO43659");
        }
    }
}