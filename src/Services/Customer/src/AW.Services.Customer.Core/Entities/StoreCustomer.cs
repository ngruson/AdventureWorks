using System.Collections.Generic;

namespace AW.Services.Customer.Core.Entities
{
    public class StoreCustomer : Customer
    {
        public override CustomerType CustomerType => CustomerType.Store;
        public string Name { get; set; }
        public string SalesPerson { get; set; }
        public List<StoreCustomerContact> Contacts { get; set; } = new List<StoreCustomerContact>();
    }
}