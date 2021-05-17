using AW.Common.AutoMapper;
using AW.Services.Customer.Application.GetCustomers;
using System.Collections.Generic;

namespace AW.Services.Customer.REST.API.Models.GetCustomers
{
    public class GetCustomersResult : IMapFrom<GetCustomersDto>
    {
        public List<Customer> Customers { get; set; }
        public int TotalCustomers { get; set; }
    }
}