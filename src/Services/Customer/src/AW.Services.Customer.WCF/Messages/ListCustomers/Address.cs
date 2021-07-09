﻿using AutoMapper;
using AW.SharedKernel.AutoMapper;
using AW.Services.Customer.Core.Handlers.GetCustomers;

namespace AW.Services.Customer.WCF.Messages.ListCustomers
{
    public class Address : IMapFrom<AddressDto>
    {
        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string PostalCode { get; set; }

        public string City { get; set; }

        public string StateProvinceCode { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AddressDto, Address>();
        }
    }
}