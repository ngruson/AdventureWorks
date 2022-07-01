namespace AW.UI.Web.SharedKernel.SalesOrder.Handlers.GetSalesOrders
{
    public class SalesOrderLine
    {
        public string CarrierTrackingNumber { get; set; }
        public short OrderQty { get; set; }
        public string ProductNumber { get; set; }
        public string ProductName { get; set; }
        public string SpecialOfferDescription { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal UnitPriceDiscount { get; set; }
        public decimal LineTotal { get; set; }
    }
}