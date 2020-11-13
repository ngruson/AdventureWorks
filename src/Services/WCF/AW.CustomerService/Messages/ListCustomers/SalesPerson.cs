﻿using AutoMapper;
using AW.Application.AutoMapper;
using AW.Application.Customer.GetCustomers;

namespace AW.CustomerService.Messages.ListCustomers
{
    public class SalesPerson : IMapFrom<SalesPersonDto>
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SalesPersonDto, SalesPerson>();
        }
    }
}