namespace AW.Services.Customer.Core.Handlers.GetCustomer
{
    public abstract class CustomerDto
    {
        public abstract CustomerType CustomerType { get; }
        public string? AccountNumber { get; set; }
        public string? Territory { get; set; }
        public List<CustomerAddressDto>? Addresses { get; set; }
        public List<SalesOrderDto>? SalesOrders { get; set; }
    }
}