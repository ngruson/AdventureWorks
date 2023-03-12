using AW.SharedKernel.Interfaces;

namespace AW.Services.Sales.Core.Handlers.UpdateSalesOrder
{
    public abstract class Customer : ICustomer
    {
        public CustomerType CustomerType { get; set; }
        public string? CustomerNumber { get; set; }
    }    
}
