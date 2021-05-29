using AW.UI.Web.Common.ApiClients.SalesOrderApi.Models;
using System;
using System.Collections.Generic;

namespace AW.UI.Web.Common.UnitTests.TestBuilders.GetSalesOrders
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

        public SalesOrderBuilder ShipMethod(string shipMethod)
        {
            salesOrder.ShipMethod = shipMethod;
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

        public SalesOrderBuilder Comment(string comment)
        {
            salesOrder.Comment = comment;
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

        public SalesOrderBuilder WithTestValues()
        {
            salesOrder = new SalesOrder
            {
                RevisionNumber = 8,
                OrderDate = new DateTime(2021, 05, 31),
                DueDate = new DateTime(2021, 06, 12),
                ShipDate = new DateTime(2021, 06, 07),
                Status = SalesOrderStatus.Shipped,
                OnlineOrderFlag = false,
                SalesOrderNumber = "SO43659",
                PurchaseOrderNumber = "PO522145787",
                AccountNumber = "10-4020-000676",
                CustomerName = "Better Bike Shop",
                SalesPerson = "Tsvi Michael Reiter",
                Territory = "Southeast",
                BillToAddress = new AddressBuilder()
                    .WithTestValues()
                    .Build(),
                ShipToAddress = new AddressBuilder()
                    .WithTestValues()
                    .Build(),
                ShipMethod = "CARGO TRANSPORT 5",
                OrderLines = new List<SalesOrderLine>
                    {
                        new SalesOrderLineBuilder()
                            .CarrierTrackingNumber("4911-403C-98")
                            .OrderQty(1)
                            .ProductNumber("BK-M82B-42")
                            .ProductName("Mountain-100 Black, 42")
                            .SpecialOfferDescription("No Discount")
                            .UnitPrice(2024.994M)
                            .UnitPriceDiscount(0)
                            .Build(),
                        new SalesOrderLineBuilder()
                            .CarrierTrackingNumber("4911-403C-98")
                            .OrderQty(3)
                            .ProductNumber("BK-M82B-44")
                            .ProductName("Mountain-100 Black, 44")
                            .SpecialOfferDescription("No Discount")
                            .UnitPrice(2024.994M)
                            .UnitPriceDiscount(0)
                            .Build()
                    }
            };

            return this;
        }

        public SalesOrder Build()
        {
            return salesOrder;
        }
    }
}