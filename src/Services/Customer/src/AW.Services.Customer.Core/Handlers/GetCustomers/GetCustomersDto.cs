using System.Collections.Generic;

namespace AW.Services.Customer.Core.Handlers.GetCustomers
{
    public class GetCustomersDto
    {
        public List<CustomerDto> Customers { get; set; }
        public int TotalCustomers { get; set; }
    }
}