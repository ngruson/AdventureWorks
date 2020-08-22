using System;

namespace AW.UI.Web.Internal.ViewModels
{
    public class SalesOrderViewModel
    {
        public DateTime OrderDate { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime? ShipDate { get; set; }

        public string Status { get; set; }

        public string OnlineOrdered { get; set; }

        public string SalesOrderNumber { get; set; }

        public string PurchaseOrderNumber { get; set; }

        public string AccountNumber { get; set; }

        public string CustomerName { get; set; }

        public string CustomerType { get; set; }

        public string SalesPerson { get; set; }

        public string Territory { get; set; }

        public decimal SubTotal { get; set; }

        public decimal TaxAmt { get; set; }

        public decimal Freight { get; set; }

        public decimal TotalDue { get; set; }
    }
}