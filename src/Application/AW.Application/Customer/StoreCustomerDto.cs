﻿using AutoMapper;
using AW.Application.AutoMapper;
using AW.Domain.Sales;
using System.Collections.Generic;

namespace AW.Application.Customer
{
    public class StoreCustomerDto : IMapFrom<Store>
    {
        public string Name { get; set; }
        public SalesPersonDto SalesPerson { get; set; }
        public List<CustomerAddressDto> Addresses { get; set; }
        public List<ContactDto> Contacts { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Store, StoreCustomerDto>()
                .ForMember(m => m.Addresses, opt => opt.MapFrom(src => src.BusinessEntityAddress))
                .ForMember(m => m.Contacts, opt => opt.MapFrom(src => src.BusinessEntityContact))
                .ReverseMap();
        }
    }
}