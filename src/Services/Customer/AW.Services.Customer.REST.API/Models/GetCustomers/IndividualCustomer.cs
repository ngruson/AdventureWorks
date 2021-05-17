﻿using AW.Common.AutoMapper;
using AW.Services.Customer.Application.GetCustomers;

namespace AW.Services.Customer.REST.API.Models.GetCustomers
{
    public class IndividualCustomer : Customer, IMapFrom<IndividualCustomerDto>
    {
        public Person Person { get; set; }
    }
}