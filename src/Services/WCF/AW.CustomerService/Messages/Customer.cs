﻿using AutoMapper;
using AW.Application.AutoMapper;
using AW.Application.Customers;
using AW.Domain.Sales;
using System.Collections.Generic;

namespace AW.CustomerService.Messages
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