namespace AW.Services.Sales.Core.Handlers.GetSalesOrders
{
    public abstract class CustomerDto
    {
        public CustomerType CustomerType { get; }
        public string CustomerNumber { get; set; }
        public string FullName { get; set; }
        public int SalesOrderCount { get; set; }
    }    
}