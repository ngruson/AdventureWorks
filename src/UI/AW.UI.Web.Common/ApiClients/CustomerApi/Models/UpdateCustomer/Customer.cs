using AW.Common.Interfaces;
using System.Collections.Generic;

namespace AW.UI.Web.Common.ApiClients.CustomerApi.Models.UpdateCustomer
{
    public abstract class Customer : ICustomer
    {
        public CustomerType CustomerType { get; set; }
        public string Territory { get; set; }
        public List<CustomerAddress> Addresses { get; set; }
    }
}