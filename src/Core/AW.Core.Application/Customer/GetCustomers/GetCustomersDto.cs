using System.Collections.Generic;

namespace AW.Core.Application.Customer.GetCustomers
{
    public class GetCustomersDto
    {
        public IEnumerable<CustomerDto> Customers { get; set; }
        public int TotalCustomers { get; set; }
    }
}