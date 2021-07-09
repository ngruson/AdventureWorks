using AW.UI.Web.Infrastructure.ApiClients.CustomerApi.Models.GetCustomer;
using System;

namespace AW.UI.Web.Infrastructure.UnitTests.TestBuilders.GetCustomer
{
    public class SalesOrderBuilder
    {
        private SalesOrder salesOrder = new SalesOrder();

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

        public SalesOrderBuilder TotalDue(decimal totalDue)
        {
            salesOrder.TotalDue = totalDue;
            return this;
        }

        public SalesOrderBuilder WithTestValues()
        {
            salesOrder = new SalesOrder
            {
                OrderDate = new DateTime(2021, 05, 31),
                DueDate = new DateTime(2021, 06, 12),
                ShipDate = new DateTime(2021, 06, 07),
                Status = SalesOrderStatus.Shipped,
                OnlineOrderFlag = false,
                SalesOrderNumber = "SO43659",
                PurchaseOrderNumber = "PO522145787",
                AccountNumber = "10-4020-000676",
                TotalDue = 23153.2339M
            };

            return this;
        }

        public SalesOrder Build()
        {
            return salesOrder;
        }
    }
}