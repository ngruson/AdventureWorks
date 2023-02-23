namespace AW.Services.Sales.Core.ValueTypes
{
    public record Address
    {
        public Address(string addressLine1, string? addressLine2, string postalCode, string city, string stateProvinceCode, string countryRegionCode)
        {
            AddressLine1 = addressLine1;
            AddressLine2 = addressLine2;
            PostalCode = postalCode;
            City = city;
            StateProvinceCode = stateProvinceCode;
            CountryRegionCode = countryRegionCode;
        }

        public string AddressLine1 { get; private init; }
        public string? AddressLine2 { get; private init; }
        public string PostalCode { get; private init; }
        public string City { get; private init; }
        public string StateProvinceCode { get; private init; }
        public string CountryRegionCode { get; private init; }
    }
}
