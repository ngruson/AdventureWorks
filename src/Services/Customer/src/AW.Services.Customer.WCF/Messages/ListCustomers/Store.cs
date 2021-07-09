﻿using AutoMapper;
using AW.SharedKernel.AutoMapper;
using AW.Services.Customer.Core.Handlers.GetCustomers;
using System.Collections.Generic;

namespace AW.Services.Customer.WCF.Messages.ListCustomers
{
    public class Store : Customer, IMapFrom<StoreCustomerDto>
    {
        public string Name { get; set; }
        
        public List<StoreCustomerContact> Contacts { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<StoreCustomerDto, Store>();
        }
    }
}