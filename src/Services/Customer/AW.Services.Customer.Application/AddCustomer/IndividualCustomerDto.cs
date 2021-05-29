﻿using AutoMapper;
using AW.Common.AutoMapper;

namespace AW.Services.Customer.Application.AddCustomer
{
    public class IndividualCustomerDto : CustomerDto, IMapFrom<Domain.IndividualCustomer>
    {
        public PersonDto Person { get; set; } = new PersonDto();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<IndividualCustomerDto, Domain.IndividualCustomer>()
                .ForMember(m => m.Id, opt => opt.Ignore())
                .ForMember(m => m.SalesOrders, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}