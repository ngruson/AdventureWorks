using System;

namespace AW.Services.Customer.Core.Entities
{
    public class SalesOrder
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; private set; }

        public DateTime DueDate { get; private set; }

        public DateTime? ShipDate { get; private set; }

        public SalesOrderStatus Status { get; private set; }

        public bool OnlineOrderFlag { get; private set; }

        public string SalesOrderNumber { get; private set; }

        public string PurchaseOrderNumber { get; private set; }

        public string AccountNumber { get; private set; }

        public decimal TotalDue { get; private set; }
    }
}