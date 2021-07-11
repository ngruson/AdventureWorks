using AutoFixture.Xunit2;
using AW.Services.SalesOrder.Core.Handlers.GetSalesOrders;
using AW.Services.SalesOrder.Core.Specifications;
using AW.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using System.Threading;
using Xunit;

namespace AW.Services.SalesOrder.Core.UnitTests
{
    public class GetSalesOrdersQueryUnitTests
    {
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async void Handle_SalesOrdersExists_ReturnSalesOrders(
            List<Entities.SalesOrder> salesOrders,
            [Frozen] Mock<IRepository<Entities.SalesOrder>> salesOrderRepoMock,
            GetSalesOrdersQueryHandler sut,
            GetSalesOrdersQuery query
        )
        {
            //Arrange
            salesOrderRepoMock.Setup(x => x.ListAsync(
                It.IsAny<GetSalesOrdersPaginatedSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(salesOrders);

            salesOrderRepoMock.Setup(x => x.CountAsync(
                It.IsAny<CountSalesOrdersSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(salesOrders.Count);

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            salesOrderRepoMock.Verify(x => x.ListAsync(
                It.IsAny<GetSalesOrdersPaginatedSpecification>(), 
                It.IsAny<CancellationToken>()
            ));

            for (int i = 0; i < result.SalesOrders.Count; i++)
            {
                result.SalesOrders[i].SalesOrderNumber.Should().Be(salesOrders[i].SalesOrderNumber);
            }
        }
    }
}