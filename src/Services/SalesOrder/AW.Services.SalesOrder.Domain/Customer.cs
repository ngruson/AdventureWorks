using System.Collections.Generic;

namespace AW.Services.SalesOrder.Domain
{
    public abstract class Customer
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public abstract string CustomerName { get; }
        public string Territory { get; set; }
        public List<CustomerAddress> Addresses { get; set; }
    }
}