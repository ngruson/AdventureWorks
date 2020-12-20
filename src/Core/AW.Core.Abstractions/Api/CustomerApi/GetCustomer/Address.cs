namespace AW.Core.Abstractions.Api.CustomerApi.GetCustomer
{
    public class Address
    {
        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string PostalCode { get; set; }

        public string City { get; set; }

        public StateProvince StateProvince { get; set; }
    }
}