namespace AW.Services.Sales.Core.Models
{
    public abstract class Customer
    {
        public CustomerType CustomerType { get; set; }
        public string CustomerNumber { get; set; }
        public string FullName { get; set; }
    }
}