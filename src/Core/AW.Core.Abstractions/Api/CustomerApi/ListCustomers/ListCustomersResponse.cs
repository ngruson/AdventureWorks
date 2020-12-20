using System.Collections.Generic;

namespace AW.Core.Abstractions.Api.CustomerApi.ListCustomers
{
    public class ListCustomersResponse
    {
        public int TotalCustomers { get; set; }
        public List<Customer> Customers { get; set; } = new List<Customer>();
    }
}