﻿using AutoMapper;
using AW.Core.Application.AutoMapper;
using AW.Core.Application.Customer.UpdateCustomer;

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