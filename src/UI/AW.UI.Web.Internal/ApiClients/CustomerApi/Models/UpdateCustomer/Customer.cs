using System.Collections.Generic;

namespace AW.UI.Web.Internal.ApiClients.CustomerApi.Models.UpdateCustomer
{
    public abstract class Customer
    {
        public abstract CustomerType CustomerType { get; }
        public string Territory { get; set; }
        public List<CustomerAddress> Addresses { get; set; }
    }
}