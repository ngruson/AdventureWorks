using System.Collections.Generic;

namespace AW.Core.Abstractions.Api.CustomerApi.UpdateCustomer
{
    public class Store
    {
        public string Name { get; set; }
        public SalesPerson SalesPerson { get; set; }
        public List<CustomerAddress> Addresses { get; set; }
        public List<CustomerContact> Contacts { get; set; }
    }
}