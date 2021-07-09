﻿using AutoMapper;
using AW.SharedKernel.AutoMapper;
using AW.Services.Customer.Core.Handlers.UpdateCustomer;

namespace AW.Services.Customer.WCF.Messages.UpdateCustomer
{
    public class CustomerAddress : IMapFrom<CustomerAddressDto>
    {
        public string AddressType { get; set; }
        public Address Address { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CustomerAddress, CustomerAddressDto>();
        }
    }
}