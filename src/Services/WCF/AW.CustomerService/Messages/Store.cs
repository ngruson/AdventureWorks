using AutoMapper;
using AW.Application.AutoMapper;
using AW.Application.Customer;
using System.Collections.Generic;

namespace AW.CustomerService.Messages
{
    public class Store : IMapFrom<StoreCustomerDto>
    {
        public string Name { get; set; }
        public SalesPerson SalesPerson { get; set; }
        public List<CustomerAddress> Addresses { get; set; }
        public List<Contact> Contacts { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<StoreCustomerDto, Store>();
        }
    }
}