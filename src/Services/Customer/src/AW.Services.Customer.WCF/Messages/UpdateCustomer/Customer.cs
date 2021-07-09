using System.Collections.Generic;

namespace AW.Services.Customer.WCF.Messages.UpdateCustomer
{
    public abstract class Customer
    {
        public string AccountNumber { get; set; }
        public string Territory { get; set; }
        public List<CustomerAddress> Addresses { get; set; }
    }
}