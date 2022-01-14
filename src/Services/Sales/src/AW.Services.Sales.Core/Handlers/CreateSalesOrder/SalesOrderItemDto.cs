namespace AW.Services.Sales.Core.Handlers.CreateSalesOrder
{
    public class SalesOrderItemDto
    {
        public string ProductNumber { get; set; }
        public string ProductName { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal Discount { get; set; }

        public short Quantity { get; set; }
    }
}