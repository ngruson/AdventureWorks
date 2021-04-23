using System.Collections.Generic;

namespace AW.Services.Customer.Domain
{
    public abstract class Customer
    {
        public int Id { get; set; }
        public abstract CustomerType CustomerType { get; }
        public string AccountNumber { get; set; }
        public string Territory { get; set; }
        public List<CustomerAddress> Addresses { get; set; }
        public List<SalesOrder> SalesOrders { get; set; }
    }
}