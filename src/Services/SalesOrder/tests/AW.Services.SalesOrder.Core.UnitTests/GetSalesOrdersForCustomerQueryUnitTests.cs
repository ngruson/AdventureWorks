using AutoFixture.Xunit2;
using AW.Services.SalesOrder.Core.Handlers.GetSalesOrdersForCustomer;
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
    public class GetSalesOrdersForCustomerQueryUnitTests
    {
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async void Handle_SalesOrderExists_ReturnSalesOrder(
            List<Entities.SalesOrder> salesOrders,
            [Frozen] Mock<IRepository<Entities.SalesOrder>> salesOrderRepoMock,
            GetSalesOrdersForCustomerQueryHandler sut,
            GetSalesOrdersForCustomerQuery query
        )
        {
            //Arrange
            salesOrderRepoMock.Setup(x => x.ListAsync(
                It.IsAny<GetSalesOrdersForCustomerSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(salesOrders);

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            salesOrderRepoMock.Verify(x => x.ListAsync(
                It.IsAny<GetSalesOrdersForCustomerSpecification>(),
                It.IsAny<CancellationToken>()
            ));

            for (int i = 0; i < result.Count; i++)
            {
                result[i].SalesOrderNumber.Should().Be(salesOrders[i].SalesOrderNumber);
            }
        }
    }
}