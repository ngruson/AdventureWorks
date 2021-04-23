using System.Collections.Generic;

namespace AW.UI.Web.Internal.ApiClients.CustomerApi.Models.GetCustomers
{
    public abstract class Customer
    {
        public string AccountNumber { get; set; }
        public virtual string CustomerName { get; }
        public string Territory { get; set; }
        public List<CustomerAddress> Addresses { get; set; }
    }
}