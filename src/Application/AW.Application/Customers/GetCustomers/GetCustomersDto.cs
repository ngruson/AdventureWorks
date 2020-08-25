using AW.Application.Customers;
using System.Collections.Generic;

namespace AW.Application.GetCustomers
{
    public class GetCustomersDto
    {
        public IEnumerable<CustomerDto> Customers { get; set; }
        public int TotalCustomers { get; set; }
    }
}