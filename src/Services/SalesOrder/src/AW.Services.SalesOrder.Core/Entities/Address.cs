namespace AW.Services.SalesOrder.Core.Entities
{
    public class Address
    {
        public int Id { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string StateProvinceCode { get; set; }
        public string CountryRegionCode { get; set; }

        public Address(string addressLine1, string addressLine2, string postalCode, string city, string stateProvinceCode, string countryRegionCode)
        {
            AddressLine1 = addressLine1;
            AddressLine2 = addressLine2;
            PostalCode = postalCode;
            City = city;
            StateProvinceCode = stateProvinceCode;
            CountryRegionCode = countryRegionCode;
        }
    }
}