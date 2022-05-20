namespace AW.Services.Sales.Core.Handlers.UpdateSalesOrder
{
    public abstract class CustomerDto
    {
        public CustomerType CustomerType { get; set; }
        public string CustomerNumber { get; set; }
        //public string FullName { get; set; }
    }    
}