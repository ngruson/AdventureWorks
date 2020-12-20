using AutoMapper;
using AW.Core.Application.AutoMapper;
using AW.Core.Application.Customer.GetCustomers;
using System.Collections.Generic;

namespace AW.CustomerService.Messages.ListCustomers
{
    public class Customer : IMapFrom<CustomerDto>
    {
        public string AccountNumber { get; set; }
        public string SalesTerritoryName { get; set; }
        public Person Person { get; set; }
        public Store Store { get; set; }
        public List<SalesOrder> SalesOrders { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CustomerDto, Customer>();
        }
    }
}