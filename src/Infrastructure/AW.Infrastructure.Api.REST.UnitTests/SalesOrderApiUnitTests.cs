using GetSalesOrder = AW.Core.Abstractions.Api.SalesOrderApi.GetSalesOrder;
using GetSalesOrder_TB = AW.Infrastructure.Api.REST.UnitTests.TestBuilders.SalesOrderApi.GetSalesOrder;
using ListSalesOrders = AW.Core.Abstractions.Api.SalesOrderApi.ListSalesOrders;
using ListSalesOrders_TB = AW.Infrastructure.Api.REST.UnitTests.TestBuilders.SalesOrderApi.ListSalesOrders;
using AW.Core.Abstractions.Api.SalesPersonApi.GetSalesPerson;
using AW.Core.Abstractions.Api.SalesPersonApi.ListSalesPersons;
using AW.Infrastructure.Http;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Xunit;
using System;

namespace AW.Infrastructure.Api.REST.UnitTests
{
    public class SalesOrderApiUnitTests
    {
        [Fact]
        public async void ListSalesOrders_ReturnsSalesOrders()
        {
            //Arrange
            var salesOrders = new List<ListSalesOrders.SalesOrder>
            {
                new ListSalesOrders_TB.SalesOrderBuilder()
                    .WithTestValues()
                    .Build(),

                new ListSalesOrders_TB.SalesOrderBuilder()
                    .RevisionNumber(8)
                    .OrderDate(new DateTime(2011, 05, 31))
                    .DueDate(new DateTime(2011, 06, 12))
                    .ShipDate(new DateTime(2011, 06, 07))
                    .Status(ListSalesOrders.SalesOrderStatus.Shipped)
                    .OnlineOrderFlag(false)
                    .SalesOrderNumber("SO43660")
                    .PurchaseOrderNumber("PO18850127500")
                    .AccountNumber("10-4020-000117")
                    .CustomerName("Pedals Warehouse")
                    .CustomerType(ListSalesOrders.CustomerType.Store)
                    .SalesPerson("Tsvi Michael Reiter")
                    .Territory("Southeast")
                    .SubTotal(1294.2529M)
                    .TaxAmt(124.2483M)
                    .Freight(38.8276M)
                    .TotalDue(1457.3288M)
                    .BillToAddress(new ListSalesOrders_TB.AddressBuilder()
                        .AddressLine1("6055 Shawnee Industrial Way")
                        .City("Suwanee")
                        .StateProvinceName("Georgia")
                        .PostalCode("30024")
                        .Build()
                    )
                    .ShipToAddress(new ListSalesOrders_TB.AddressBuilder()
                        .AddressLine1("6055 Shawnee Industrial Way")
                        .City("Suwanee")
                        .StateProvinceName("Georgia")
                        .PostalCode("30024")
                        .Build()
                    )
                    .ShipMethod(new ListSalesOrders_TB.ShipMethodBuilder().WithTestValues().Build())
                    .OrderLines(new List<ListSalesOrders.SalesOrderLine>
                        {
                            new ListSalesOrders_TB.SalesOrderLineBuilder()
                                .CarrierTrackingNumber("6431-4D57-83")
                                .OrderQty(1)
                                .ProductName("Road-650 Red, 44")
                                .SpecialOfferDescription("No Discount")
                                .UnitPrice(419.4589M)
                                .UnitPriceDiscount(0.00M)
                                .LineTotal(419.4589M)
                                .Build(),

                            new ListSalesOrders_TB.SalesOrderLineBuilder()
                                .CarrierTrackingNumber("6431-4D57-83")
                                .OrderQty(1)
                                .ProductName("Road-450 Red, 52")
                                .SpecialOfferDescription("No Discount")
                                .UnitPrice(874.794M)
                                .UnitPriceDiscount(0.00M)
                                .LineTotal(874.794M)
                                .Build()
                        }
                    )
                    .Build()
            };

            var mockLogger = new Mock<ILogger<SalesOrderApi>>();
            var mockHttpRequestFactory = new Mock<IHttpRequestFactory>();
            mockHttpRequestFactory.Setup(x => x.Get(
                    It.IsAny<string>(), It.IsAny<Dictionary<string, string>>(), It.IsAny<string>()
                )
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new JsonContent(new ListSalesOrders.ListSalesOrdersResponse
                {
                    SalesOrders = salesOrders
                })
            });

            var baseAddress = "BaseAddress";

            var sut = new SalesOrderApi(
                mockLogger.Object,
                mockHttpRequestFactory.Object,
                baseAddress
            );

            //Act
            var response = await sut.ListSalesOrdersAsync(new ListSalesOrders.ListSalesOrdersRequest());

            //Assert
            mockHttpRequestFactory.Verify(x => x.Get(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>(), It.IsAny<string>()));
            response.SalesOrders[0].SalesOrderNumber.Should().Be("SO43659");
            response.SalesOrders[1].SalesOrderNumber.Should().Be("SO43660");
        }

        [Fact]
        public async void GetSalesOrder_ReturnsSalesOrder()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<SalesOrderApi>>();
            var mockHttpRequestFactory = new Mock<IHttpRequestFactory>();
            mockHttpRequestFactory.Setup(x => x.Get(
                    It.IsAny<string>(), It.IsAny<string>()
                )
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new JsonContent(new GetSalesOrder.GetSalesOrderResponse
                {
                    SalesOrder = new GetSalesOrder_TB.SalesOrderBuilder().WithTestValues().Build()
                })
            });

            var baseAddress = "BaseAddress";

            var sut = new SalesOrderApi(
                mockLogger.Object,
                mockHttpRequestFactory.Object,
                baseAddress
            );

            //Act
            var response = await sut.GetSalesOrderAsync(new GetSalesOrder.GetSalesOrderRequest());

            //Assert
            mockHttpRequestFactory.Verify(x => x.Get(It.IsAny<string>(), It.IsAny<string>()));

            response.SalesOrder.SalesOrderNumber.Should().Be("SO43659");
        }
    }
}