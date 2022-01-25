namespace AW.Services.Sales.Core.Handlers.GetSalesOrders
{
    public class CustomerDto
    {
        public CustomerType CustomerType { get; }
        public string CustomerNumber { get; set; }
        public string FullName { get; set; }
    }    
}