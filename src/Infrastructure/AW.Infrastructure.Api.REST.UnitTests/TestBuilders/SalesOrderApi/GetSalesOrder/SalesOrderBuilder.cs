using AW.Core.Abstractions.Api.SalesOrderApi.GetSalesOrder;
using System;
using System.Collections.Generic;

namespace AW.Infrastructure.Api.REST.UnitTests.TestBuilders.SalesOrderApi.GetSalesOrder
{
    public class SalesOrderBuilder
    {
        private SalesOrder salesOrder = new SalesOrder();

        public SalesOrderBuilder RevisionNumber(byte revisionNumber)
        {
            salesOrder.RevisionNumber = revisionNumber;
            return this;
        }

        public SalesOrderBuilder OrderDate(DateTime orderDate)
        {
            salesOrder.OrderDate = orderDate;
            return this;
        }

        public SalesOrderBuilder DueDate(DateTime dueDate)
        {
            salesOrder.DueDate = dueDate;
            return this;
        }

        public SalesOrderBuilder ShipDate(DateTime shipDate)
        {
            salesOrder.ShipDate = shipDate;
            return this;
        }

        public SalesOrderBuilder Status(SalesOrderStatus status)
        {
            salesOrder.Status = status;
            return this;
        }

        public SalesOrderBuilder OnlineOrderFlag(bool onlineOrderFlag)
        {
            salesOrder.OnlineOrderFlag = onlineOrderFlag;
            return this;
        }

        public SalesOrderBuilder SalesOrderNumber(string salesOrderNumber)
        {
            salesOrder.SalesOrderNumber = salesOrderNumber;
            return this;
        }

        public SalesOrderBuilder PurchaseOrderNumber(string purchaseOrderNumber)
        {
            salesOrder.PurchaseOrderNumber = purchaseOrderNumber;
            return this;
        }

        public SalesOrderBuilder AccountNumber(string accountNumber)
        {
            salesOrder.AccountNumber = accountNumber;
            return this;
        }

        public SalesOrderBuilder CustomerName(string customerName)
        {
            salesOrder.CustomerName = customerName;
            return this;
        }

        public SalesOrderBuilder CustomerType(CustomerType customerType)
        {
            salesOrder.CustomerType = customerType;
            return this;
        }

        public SalesOrderBuilder SalesPerson(string salesPerson)
        {
            salesOrder.SalesPerson = salesPerson;
            return this;
        }

        public SalesOrderBuilder Territory(string territory)
        {
            salesOrder.Territory = territory;
            return this;
        }

        public SalesOrderBuilder SubTotal(decimal subTotal)
        {
            salesOrder.SubTotal = subTotal;
            return this;
        }

        public SalesOrderBuilder TaxAmt(decimal taxAmt)
        {
            salesOrder.TaxAmt = taxAmt;
            return this;
        }

        public SalesOrderBuilder Freight(decimal freight)
        {
            salesOrder.Freight = freight;
            return this;
        }

        public SalesOrderBuilder TotalDue(decimal totalDue)
        {
            salesOrder.TotalDue = totalDue;
            return this;
        }

        public SalesOrderBuilder BillToAddress(Address billToAddress)
        {
            salesOrder.BillToAddress = billToAddress;
            return this;
        }

        public SalesOrderBuilder ShipToAddress(Address shipToAddress)
        {
            salesOrder.ShipToAddress = shipToAddress;
            return this;
        }

        public SalesOrderBuilder ShipMethod(ShipMethod shipMethod)
        {
            salesOrder.ShipMethod = shipMethod;
            return this;
        }

        public SalesOrderBuilder OrderLines(List<SalesOrderLine> orderLines)
        {
            salesOrder.OrderLines = orderLines;
            return this;
        }

        public SalesOrderBuilder SalesReasons(List<SalesReason> salesReasons)
        {
            salesOrder.SalesReasons = salesReasons;
            return this;
        }

        public SalesOrder Build()
        {
            return salesOrder;
        }

        public SalesOrderBuilder WithTestValues()
        {
            salesOrder = new SalesOrder
            {
                RevisionNumber = 8,
                OrderDate = new DateTime(2011, 05, 31),
                DueDate = new DateTime(2011, 06, 12),
                ShipDate = new DateTime(2011, 06, 07),
                Status = SalesOrderStatus.Shipped,
                OnlineOrderFlag = false,
                SalesOrderNumber = "SO43659",
                PurchaseOrderNumber = "PO522145787",
                AccountNumber = "10-4020-000676",
                CustomerName = "Better Bike Shop",
                CustomerType = Core.Abstractions.Api.SalesOrderApi.GetSalesOrder.CustomerType.Store,
                SalesPerson = "Tsvi Michael Reiter",
                Territory = "Southeast",
                SubTotal = 20565.6206M,
                TaxAmt = 1971.5149M,
                Freight = 616.0984M,
                TotalDue = 23153.2339M,
                BillToAddress = new AddressBuilder().WithTestValues().Build(),
                ShipToAddress = new AddressBuilder().WithTestValues().Build(),
                ShipMethod = new ShipMethodBuilder().WithTestValues().Build(),
                OrderLines = BuildOrderLines(),
                SalesReasons = BuildSalesReasons()
            };

            return this;
        }

        private List<SalesOrderLine> BuildOrderLines()
        {
            return new List<SalesOrderLine>
            {
                new SalesOrderLineBuilder()
                    .CarrierTrackingNumber("4911-403C-98")
                    .OrderQty(1)
                    .ProductName("Mountain-100 Black, 42")
                    .SpecialOfferDescription("No Discount")
                    .UnitPrice(2024.994M)
                    .UnitPriceDiscount(0M)
                    .LineTotal(2024.994000M)
                    .Build(),
                new SalesOrderLineBuilder()
                    .CarrierTrackingNumber("4911-403C-98")
                    .OrderQty(3)
                    .ProductName("Mountain-100 Black, 44")
                    .SpecialOfferDescription("No Discount")
                    .UnitPrice(2024.994M)
                    .UnitPriceDiscount(0M)
                    .LineTotal(6074.982000M)
                    .Build(),
                new SalesOrderLineBuilder()
                    .CarrierTrackingNumber("4911-403C-98")
                    .OrderQty(1)
                    .ProductName("Mountain-100 Black, 48")
                    .SpecialOfferDescription("No Discount")
                    .UnitPrice(2024.994M)
                    .UnitPriceDiscount(0M)
                    .LineTotal(2024.994M)
                    .Build(),
                new SalesOrderLineBuilder()
                    .CarrierTrackingNumber("4911-403C-98")
                    .OrderQty(1)
                    .ProductName("BK-M82S-38")
                    .SpecialOfferDescription("No Discount")
                    .UnitPrice(2039.994M)
                    .UnitPriceDiscount(0M)
                    .LineTotal(2039.994M)
                    .Build(),
                new SalesOrderLineBuilder()
                    .CarrierTrackingNumber("4911-403C-98")
                    .OrderQty(1)
                    .ProductName("BK-M82S-42")
                    .SpecialOfferDescription("No Discount")
                    .UnitPrice(2039.994M)
                    .UnitPriceDiscount(0M)
                    .LineTotal(2039.994M)
                    .Build(),
                new SalesOrderLineBuilder()
                    .CarrierTrackingNumber("4911-403C-98")
                    .OrderQty(2)
                    .ProductName("BK-M82S-44")
                    .SpecialOfferDescription("No Discount")
                    .UnitPrice(2039.994M)
                    .UnitPriceDiscount(0M)
                    .LineTotal(4079.988000M)
                    .Build(),
                new SalesOrderLineBuilder()
                    .CarrierTrackingNumber("4911-403C-98")
                    .OrderQty(1)
                    .ProductName("BK-M82S-48")
                    .SpecialOfferDescription("No Discount")
                    .UnitPrice(2039.994M)
                    .UnitPriceDiscount(0M)
                    .LineTotal(2039.994M)
                    .Build(),
                new SalesOrderLineBuilder()
                    .CarrierTrackingNumber("4911-403C-98")
                    .OrderQty(3)
                    .ProductName("LJ-0192-M")
                    .SpecialOfferDescription("No Discount")
                    .UnitPrice(28.8404M)
                    .UnitPriceDiscount(0M)
                    .LineTotal(86.521200M)
                    .Build(),
                new SalesOrderLineBuilder()
                    .CarrierTrackingNumber("4911-403C-98")
                    .OrderQty(1)
                    .ProductName("LJ-0192-X")
                    .SpecialOfferDescription("No Discount")
                    .UnitPrice(28.8404M)
                    .UnitPriceDiscount(0M)
                    .LineTotal(28.8404M)
                    .Build(),
                new SalesOrderLineBuilder()
                    .CarrierTrackingNumber("4911-403C-98")
                    .OrderQty(6)
                    .ProductName("SO-B909-M")
                    .SpecialOfferDescription("No Discount")
                    .UnitPrice(5.70M)
                    .UnitPriceDiscount(0M)
                    .LineTotal(34.200000M)
                    .Build(),
                new SalesOrderLineBuilder()
                    .CarrierTrackingNumber("4911-403C-98")
                    .OrderQty(2)
                    .ProductName("CA-1098")
                    .SpecialOfferDescription("No Discount")
                    .UnitPrice(5.1865M)
                    .UnitPriceDiscount(0M)
                    .LineTotal(10.373000M)
                    .Build(),
                new SalesOrderLineBuilder()
                    .CarrierTrackingNumber("4911-403C-98")
                    .OrderQty(4)
                    .ProductName("HL-U509-B")
                    .SpecialOfferDescription("No Discount")
                    .UnitPrice(20.1865M)
                    .UnitPriceDiscount(0M)
                    .LineTotal(80.746000M)
                    .Build()
            };
        }

        private List<SalesReason> BuildSalesReasons()
        {
            return new List<SalesReason>
            {
                new SalesReasonBuilder().WithTestValues().Build()
            };
        }
    }
}