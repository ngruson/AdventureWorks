namespace AW.Services.Customer.Core.Handlers.AddCustomer
{
    public abstract class CustomerDto
    {
        public string? AccountNumber { get; set; }
        public string? Territory { get; set; }
        public List<CustomerAddressDto> Addresses { get; set; } = new();
    }
}