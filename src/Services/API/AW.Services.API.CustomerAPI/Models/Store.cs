using AutoMapper;
using AW.Application.AutoMapper;
using AW.Application.Customers;
using System.Collections.Generic;

namespace AW.Services.API.CustomerAPI.Models
{
    public class Store : IMapFrom<StoreDto>
    {
        public string Name { get; set; }
        public string SalesPerson { get; set; }
        public List<CustomerAddress> Addresses { get; set; }
        public List<Contact> Contacts { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<StoreDto, Store>();
        }
    }
}