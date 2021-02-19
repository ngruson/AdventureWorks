using System.Collections.Generic;

namespace AW.Services.Customer.Application.GetCustomers
{
    public class GetCustomersDto
    {
        public List<CustomerDto> Customers { get; set; }
        public int TotalCustomers { get; set; }
    }
}