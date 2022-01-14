using AutoFixture.Xunit2;
using AW.Services.Sales.Core.Handlers.GetSalesOrder;
using AW.Services.Sales.Core.Specifications;
using AW.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.Sales.Core.UnitTests.Handlers
{
    public class GetSalesOrderQueryUnitTests
    {
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task Handle_SalesOrderExists_ReturnSalesOrder(
            Core.Entities.SalesOrder salesOrder,
            [Frozen] Mock<IRepository<Core.Entities.SalesOrder>> salesOrderRepoMock,
            GetSalesOrderQueryHandler sut,
            GetSalesOrderQuery query
        )
        {
            //Arrange
            salesOrderRepoMock.Setup(x => x.GetBySpecAsync(
                It.IsAny<GetFullSalesOrderSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(salesOrder);

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            salesOrderRepoMock.Verify(x => x.GetBySpecAsync(
                It.IsAny<GetFullSalesOrderSpecification>(),
                It.IsAny<CancellationToken>()
            ));
            result.SalesOrderNumber.Should().Be(salesOrder.SalesOrderNumber);
        }
    }
}