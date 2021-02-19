using System.Collections.Generic;

namespace AW.Services.Customer.Domain
{
    public class StoreCustomer : Customer
    {
        public string Name { get; set; }
        public string SalesPersonName { get; set; }
        public List<StoreCustomerContact> Contacts { get; set; } = new List<StoreCustomerContact>();
    }
}