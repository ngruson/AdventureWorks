namespace AW.Services.Customer.Core.Handlers.DeleteCustomerAddress
{
    public class AddressDto
    {
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? PostalCode { get; set; }
        public string? City { get; set; }
        public string? StateProvinceCode { get; set; }
        public string? CountryRegionCode { get; set; }
    }
}