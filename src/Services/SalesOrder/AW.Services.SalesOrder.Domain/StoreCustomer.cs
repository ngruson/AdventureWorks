using System.Collections.Generic;

namespace AW.Services.SalesOrder.Domain
{
    public class StoreCustomer : Customer
    {
        public string Name { get; set; }
        public override string CustomerName => Name;
        public string SalesPersonName { get; set; }
        public List<StoreCustomerContact> Contacts { get; set; } = new List<StoreCustomerContact>();
    }
}