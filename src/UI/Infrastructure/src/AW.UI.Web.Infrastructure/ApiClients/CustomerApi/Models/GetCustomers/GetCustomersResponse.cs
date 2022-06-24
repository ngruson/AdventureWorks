using System.Collections.Generic;

namespace AW.UI.Web.Infrastructure.ApiClients.CustomerApi.Models.GetCustomers
{
    public class GetCustomersResponse
    {
        public List<Customer> Customers { get; set; }
        public int TotalCustomers { get; set; }
    }
}