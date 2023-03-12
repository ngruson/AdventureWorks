using AW.SharedKernel.Interfaces;

namespace AW.Services.Sales.Core.Handlers.GetSalesOrder
{
    public abstract class Customer : ICustomer
    {
        public CustomerType CustomerType { get; set; }
        public string? CustomerNumber { get; set; }
        public abstract string? CustomerName { get; }
        public int SalesOrderCount { get; set; }
    }    
}
