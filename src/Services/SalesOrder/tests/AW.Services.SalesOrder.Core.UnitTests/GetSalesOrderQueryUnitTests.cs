using AutoFixture.Xunit2;
using AW.Services.SalesOrder.Core.Handlers.GetSalesOrder;
using AW.Services.SalesOrder.Core.Specifications;
using AW.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Moq;
using System.Threading;
using Xunit;

namespace AW.Services.SalesOrder.Core.UnitTests
{
    public class GetSalesOrderQueryUnitTests
    {
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async void Handle_SalesOrderExists_ReturnSalesOrder(
            Entities.SalesOrder salesOrder,
            [Frozen] Mock<IRepository<Entities.SalesOrder>> salesOrderRepoMock,
            GetSalesOrderQueryHandler sut,
            GetSalesOrderQuery query
        )
        {
            //Arrange
            salesOrderRepoMock.Setup(x => x.GetBySpecAsync(
                It.IsAny<GetSalesOrderSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(salesOrder);

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            salesOrderRepoMock.Verify(x => x.GetBySpecAsync(
                It.IsAny<GetSalesOrderSpecification>(),
                It.IsAny<CancellationToken>()
            ));
            result.SalesOrderNumber.Should().Be(salesOrder.SalesOrderNumber);
        }
    }
}