using AW.Services.Customer.Core.Handlers.GetCustomers;
using AW.SharedKernel.AutoMapper;
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