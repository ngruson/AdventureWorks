namespace AW.UI.Web.Infrastructure.Api.Customer.Handlers.GetStoreCustomer;

public class Address
{
    public Guid ObjectId { get; set; }
    public string? AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public string? PostalCode { get; set; }
    public string? City { get; set; }
    public string? StateProvinceCode { get; set; }
    public string? CountryRegionCode { get; set; }
}
