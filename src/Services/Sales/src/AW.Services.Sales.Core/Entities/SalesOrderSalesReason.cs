namespace AW.Services.Sales.Core.Entities
{
    public class SalesOrderSalesReason
    {
        public int Id { get; set; }
        public int SalesOrderID { get; set; }
        public int SalesReasonID { get; set; }
        public SalesReason SalesReason { get; set; }
    }
}