using System.Collections.Generic;

namespace AW.Services.Customer.Domain
{
    public abstract class Customer
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public string TerritoryName { get; set; }
        public List<CustomerAddress> Addresses { get; set; }
    }
}