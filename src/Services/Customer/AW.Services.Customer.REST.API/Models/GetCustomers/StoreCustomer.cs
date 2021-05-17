using AW.Common.AutoMapper;
using AW.Services.Customer.Application.GetCustomers;
using System.Collections.Generic;

namespace AW.Services.Customer.REST.API.Models.GetCustomers
{
    public class StoreCustomer : Customer, IMapFrom<StoreCustomerDto>
    {
        public string Name { get; set; }
        public string SalesPerson { get; set; }
        public List<StoreCustomerContact> Contacts { get; set; }
    }
}