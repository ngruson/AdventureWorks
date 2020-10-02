﻿using AutoMapper;
using AW.Application.AutoMapper;
using AW.Application.Customer;

namespace AW.CustomerService.Messages.UpdateCustomer
{
    public class UpdateSalesPerson : IMapFrom<SalesPersonDto>
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SalesPersonDto, UpdateSalesPerson>()
                .ReverseMap();
        }
    }
}