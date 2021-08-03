using AutoFixture.Xunit2;
using AW.Services.SalesOrder.Core.Handlers.GetSalesOrder;
using AW.Services.SalesOrder.Core.Handlers.GetSalesOrders;
using AW.Services.SalesOrder.WCF.Messages.GetSalesOrder;
using AW.Services.SalesOrder.WCF.Messages.ListSalesOrders;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using MediatR;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.SalesOrder.WCF.UnitTests
{
    public class SalesOrderServiceUnitTests
    {
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task ListSalesOrders_ReturnsSalesOrders(
            [Frozen] Mock<IMediator> mockMediator,
            List<Core.Handlers.GetSalesOrders.SalesOrderDto> salesOrders,
            SalesOrderService sut,
            ListSalesOrdersRequest request
        )
        {
            //Arrange
            var dto = new GetSalesOrdersDto
            {
                SalesOrders = salesOrders,
                TotalSalesOrders = salesOrders.Count
            };

            mockMediator.Setup(x => x.Send(
                It.IsAny<GetSalesOrdersQuery>(), 
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(dto);

            //Act
            var result = await sut.ListSalesOrders(request);

            //Assert
            result.Should().NotBeNull();
            result.SalesOrders.SalesOrder.Count().Should().Be(salesOrders.Count);
            result.TotalSalesOrders.Should().Be(salesOrders.Count);
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task GetSalesOrder_ReturnSalesOrder(
            [Frozen] Mock<IMediator> mockMediator,
            Core.Handlers.GetSalesOrder.SalesOrderDto salesOrder,
            SalesOrderService sut,
            GetSalesOrderRequest request
        )
        {
            //Arrange
            mockMediator.Setup(x => x.Send(
                It.IsAny<GetSalesOrderQuery>(), 
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(salesOrder);

            //Act
            var result = await sut.GetSalesOrder(request);

            //Assert
            result.Should().NotBeNull();
            result.SalesOrder.Should().NotBeNull();
            result.SalesOrder.Should().BeEquivalentTo(salesOrder);
        }
    }
}