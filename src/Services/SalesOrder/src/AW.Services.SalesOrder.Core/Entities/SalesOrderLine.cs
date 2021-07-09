namespace AW.Services.SalesOrder.Core.Entities
{
    public class SalesOrderLine
    {
        public int Id { get; set; }
        public int SalesOrderID { get; set; }
        public string CarrierTrackingNumber { get; set; }
        public short OrderQty { get; set; }
        public string ProductNumber { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal UnitPriceDiscount { get; set; }
        public decimal LineTotal => (UnitPrice - UnitPriceDiscount) * OrderQty;
        public SpecialOfferProduct SpecialOfferProduct { get; set; }
    }
}