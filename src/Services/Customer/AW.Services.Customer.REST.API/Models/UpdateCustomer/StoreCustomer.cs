using AutoMapper;
using AW.Services.Customer.Application.Common;
using AW.Services.Customer.Application.UpdateCustomer;
using System.Collections.Generic;

namespace AW.Services.Customer.REST.API.Models.UpdateCustomer
{
    public class StoreCustomer : Customer, IMapFrom<StoreCustomerDto>
    {
        public override CustomerType CustomerType => CustomerType.Store;
        public string Name { get; set; }
        public string SalesPerson { get; set; }
        public List<StoreCustomerContact> Contacts { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<StoreCustomer, StoreCustomerDto>()
                .ReverseMap();
        }
    }
}