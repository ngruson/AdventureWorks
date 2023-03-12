namespace AW.Services.Sales.Core.Handlers.GetSalesOrders
{
    public abstract class CustomerDto
    {
        public CustomerType CustomerType { get; }
        public string? CustomerNumber { get; set; }
        public abstract string? CustomerName { get; }
        public int SalesOrderCount { get; set; }
    }    
}
