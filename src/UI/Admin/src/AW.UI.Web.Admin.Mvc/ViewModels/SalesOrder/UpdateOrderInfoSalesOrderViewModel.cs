using System;

namespace AW.UI.Web.Admin.Mvc.ViewModels.SalesOrder
{
    public class UpdateOrderInfoSalesOrderViewModel
    {
        public string SalesOrderNumber { get; set; }
        public string RevisionNumber { get; set; }
        public bool OnlineOrderFlag { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ShipDate { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public string AccountNumber { get; set; }
        public string ShipMethod { get; set; }
        public string Territory { get; set; }
        public string SalesPerson { get; set; }
    }
}