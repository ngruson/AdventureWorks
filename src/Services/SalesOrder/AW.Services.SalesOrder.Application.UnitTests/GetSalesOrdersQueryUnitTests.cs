using Ardalis.Specification;
using AW.Services.SalesOrder.Application.GetSalesOrders;
using AW.Services.SalesOrder.Application.Specifications;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Threading;
using Xunit;

namespace AW.Services.SalesOrder.Application.UnitTests
{
    public class GetSalesOrdersQueryUnitTests
    {
        [Fact]
        public async void Handle_SalesOrdersExists_ReturnSalesOrders()
        {
            var mapper = Mapper.CreateMapper();
            var loggerMock = new Mock<ILogger<GetSalesOrdersQueryHandler>>();
            var salesOrderRepoMock = new Mock<IRepositoryBase<Domain.SalesOrder>>();

            salesOrderRepoMock.Setup(x => x.ListAsync(It.IsAny< GetSalesOrdersPaginatedSpecification>()))
                .ReturnsAsync(new List<Domain.SalesOrder>
                {
                    new TestBuilders.SalesOrderBuilder()
                        .WithTestValues()
                        .Build(),

                    new TestBuilders.SalesOrderBuilder()
                        .SalesOrderNumber("SO43660")
                        .Build()
                });

            salesOrderRepoMock.Setup(x => x.CountAsync(It.IsAny<CountSalesOrdersSpecification>()))
                .ReturnsAsync(2);

            var handler = new GetSalesOrdersQueryHandler(
                loggerMock.Object,
                salesOrderRepoMock.Object,
                mapper
            );

            //Act
            var query = new GetSalesOrdersQuery();
            var result = await handler.Handle(query, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            salesOrderRepoMock.Verify(x => x.ListAsync(It.IsAny< GetSalesOrdersPaginatedSpecification>()));
            
            result.SalesOrders[0].SalesOrderNumber.Should().Be("SO43659");
            result.SalesOrders[1].SalesOrderNumber.Should().Be("SO43660");
        }

        [Fact]
        public async void Handle_SalesOrdersExists_ReturnSalesOrdersWithBillToAddress()
        {
            var mapper = Mapper.CreateMapper();
            var loggerMock = new Mock<ILogger<GetSalesOrdersQueryHandler>>();
            var salesOrderRepoMock = new Mock<IRepositoryBase<Domain.SalesOrder>>();

            var address = new TestBuilders.AddressBuilder()
                .AddressLine1("6055 Shawnee Industrial Way")
                .PostalCode("30024")
                .City("Suwanee")
                .StateProvinceCode("GA")
                .CountryRegionCode("US")
                .Build();

            salesOrderRepoMock.Setup(x => x.ListAsync(It.IsAny<GetSalesOrdersPaginatedSpecification>()))
                .ReturnsAsync(new List<Domain.SalesOrder>
                {
                    new TestBuilders.SalesOrderBuilder()
                        .WithTestValues()
                        .Build(),

                    new TestBuilders.SalesOrderBuilder()
                        .SalesOrderNumber("SO43660")
                        .BillToAddress(address)
                        .Build()
                });

            salesOrderRepoMock.Setup(x => x.CountAsync(It.IsAny<CountSalesOrdersSpecification>()))
                .ReturnsAsync(2);

            var handler = new GetSalesOrdersQueryHandler(
                loggerMock.Object,
                salesOrderRepoMock.Object,
                mapper
            );

            //Act
            var query = new GetSalesOrdersQuery();
            var result = await handler.Handle(query, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            salesOrderRepoMock.Verify(x => x.ListAsync(It.IsAny<GetSalesOrdersPaginatedSpecification>()));

            var salesOrder = result.SalesOrders[0];
            salesOrder.BillToAddress.AddressLine1.Should().Be("42525 Austell Road");
            salesOrder.BillToAddress.PostalCode.Should().Be("30106");
            salesOrder.BillToAddress.City.Should().Be("Austell");
            salesOrder.BillToAddress.StateProvinceCode.Should().Be("GA");
            salesOrder.BillToAddress.CountryRegionCode.Should().Be("US");

            salesOrder = result.SalesOrders[1];
            salesOrder.BillToAddress.AddressLine1.Should().Be("6055 Shawnee Industrial Way");
            salesOrder.BillToAddress.PostalCode.Should().Be("30024");
            salesOrder.BillToAddress.City.Should().Be("Suwanee");
            salesOrder.BillToAddress.StateProvinceCode.Should().Be("GA");
            salesOrder.BillToAddress.CountryRegionCode.Should().Be("US");
        }

        [Fact]
        public async void Handle_SalesOrdersExists_ReturnSalesOrdersWithShipToAddress()
        {
            var mapper = Mapper.CreateMapper();
            var loggerMock = new Mock<ILogger<GetSalesOrdersQueryHandler>>();
            var salesOrderRepoMock = new Mock<IRepositoryBase<Domain.SalesOrder>>();

            var address = new TestBuilders.AddressBuilder()
                .AddressLine1("6055 Shawnee Industrial Way")
                .PostalCode("30024")
                .City("Suwanee")
                .StateProvinceCode("GA")
                .CountryRegionCode("US")
                .Build();

            salesOrderRepoMock.Setup(x => x.ListAsync(It.IsAny<GetSalesOrdersPaginatedSpecification>()))
                .ReturnsAsync(new List<Domain.SalesOrder>
                {
                    new TestBuilders.SalesOrderBuilder()
                        .WithTestValues()
                        .Build(),

                    new TestBuilders.SalesOrderBuilder()
                        .SalesOrderNumber("SO43660")
                        .ShipToAddress(address)
                        .Build()
                });

            salesOrderRepoMock.Setup(x => x.CountAsync(It.IsAny<CountSalesOrdersSpecification>()))
                .ReturnsAsync(2);

            var handler = new GetSalesOrdersQueryHandler(
                loggerMock.Object,
                salesOrderRepoMock.Object,
                mapper
            );

            //Act
            var query = new GetSalesOrdersQuery();
            var result = await handler.Handle(query, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            salesOrderRepoMock.Verify(x => x.ListAsync(It.IsAny<GetSalesOrdersPaginatedSpecification>()));

            var salesOrder = result.SalesOrders[0];
            salesOrder.ShipToAddress.AddressLine1.Should().Be("42525 Austell Road");
            salesOrder.ShipToAddress.PostalCode.Should().Be("30106");
            salesOrder.ShipToAddress.City.Should().Be("Austell");
            salesOrder.ShipToAddress.StateProvinceCode.Should().Be("GA");
            salesOrder.ShipToAddress.CountryRegionCode.Should().Be("US");

            salesOrder = result.SalesOrders[1];
            salesOrder.ShipToAddress.AddressLine1.Should().Be("6055 Shawnee Industrial Way");
            salesOrder.ShipToAddress.PostalCode.Should().Be("30024");
            salesOrder.ShipToAddress.City.Should().Be("Suwanee");
            salesOrder.ShipToAddress.StateProvinceCode.Should().Be("GA");
            salesOrder.ShipToAddress.CountryRegionCode.Should().Be("US");
        }
    }
}