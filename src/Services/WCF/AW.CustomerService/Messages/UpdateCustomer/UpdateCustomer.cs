﻿using AutoMapper;
using AW.Application.AutoMapper;
using AW.Application.Customer.UpdateCustomer;

namespace AW.CustomerService.Messages.UpdateCustomer
{
    public class UpdateCustomer : IMapFrom<CustomerDto>
    {
        public string AccountNumber { get; set; }
        public string SalesTerritoryName { get; set; }
        public UpdatePerson Person { get; set; }
        public UpdateStore Store { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateCustomer, CustomerDto>()
                .ForMember(m => m.SalesOrders, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}