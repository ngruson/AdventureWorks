using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AW.UI.Web.Internal.ApiClients.CustomerApi.Models.GetCustomers
{
    public abstract class Customer
    {
        public CustomerType CustomerType { get; set; }
        public string AccountNumber { get; set; }

        [JsonIgnore]
        public virtual string CustomerName { get; }
        public string Territory { get; set; }
        public List<CustomerAddress> Addresses { get; set; }
    }
}