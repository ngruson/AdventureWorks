using AW.SharedKernel.Interfaces;

namespace AW.Services.Customer.Core.Entities
{
    public class Address : IAggregateRoot
    {
        private int Id { get; set; }
        public string AddressLine1 { get; private set; }
        public string AddressLine2 { get; private set; }
        public string PostalCode { get; private set; }
        public string City { get; private set; }
        public string StateProvinceCode { get; private set; }
        public string CountryRegionCode { get; private set; }
    }
}