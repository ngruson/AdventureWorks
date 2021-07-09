using AW.SharedKernel.Interfaces;

namespace AW.Services.Customer.Core.Entities
{
    public class Address : IAggregateRoot
    {
        public int Id { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }       
        public string StateProvinceCode { get; set; }
        public string CountryRegionCode { get; set; }
    }
}