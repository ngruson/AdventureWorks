﻿using AutoMapper;
using AW.Core.Application.AutoMapper;
using AW.Core.Application.Customer.GetCustomers;

namespace AW.CustomerService.Messages.ListCustomers
{
    public class Address : IMapFrom<AddressDto>
    {
        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string PostalCode { get; set; }

        public string City { get; set; }

        public StateProvince StateProvince { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AddressDto, Address>();
        }
    }
}