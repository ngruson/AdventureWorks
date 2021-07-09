using AW.Services.Customer.Core.Handlers.GetCustomers;
using AW.SharedKernel.AutoMapper;
using System.Collections.Generic;

namespace AW.Services.Customer.REST.API.Models.GetCustomers
{
    public class GetCustomersResult : IMapFrom<GetCustomersDto>
    {
        public List<Customer> Customers { get; set; }
        public int TotalCustomers { get; set; }
    }
}