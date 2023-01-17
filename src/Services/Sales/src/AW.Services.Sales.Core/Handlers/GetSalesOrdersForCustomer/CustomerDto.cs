namespace AW.Services.Sales.Core.Handlers.GetSalesOrdersForCustomer
{
    public abstract class CustomerDto
    {
        public CustomerType CustomerType { get; }
        public string CustomerNumber { get; set; }
        public string FullName { get; set; }
        public string Territory { get; set; }
        public int SalesOrderCount { get; set; }
    }    
}