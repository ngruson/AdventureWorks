using Ardalis.Specification;
using AW.Application.SalesOrder.GetSalesOrder;
using AW.Application.Specifications;
using AW.Application.UnitTests.AutoMapper;
using AW.Application.UnitTests.TestBuilders;
using FluentAssertions;
using Moq;
using System.Threading;
using Xunit;

namespace AW.Application.UnitTests
{
    public class GetSalesOrderQueryHandlerUnitTests
    {
        [Fact]
        public async void Handle_ProductExists_ReturnProduct()
        {
            // Arrange
            var mapper = Mapper.CreateMapper();
            var salesOrder = new SalesOrderBuilder().WithTestValues().Build();

            var productRepoMock = new Mock<IRepositoryBase<Domain.Sales.SalesOrderHeader>>();
            productRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetSalesOrderSpecification>()))
                .ReturnsAsync(salesOrder);

            var handler = new GetSalesOrderQueryHandler(
                productRepoMock.Object,
                mapper
            );

            //Act
            var query = new GetSalesOrderQuery();
            var result = await handler.Handle(query, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
        }
    }
}