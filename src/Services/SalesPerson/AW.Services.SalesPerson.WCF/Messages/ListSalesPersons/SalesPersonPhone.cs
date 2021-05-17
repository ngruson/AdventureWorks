﻿using AutoMapper;
using AW.Common.AutoMapper;
using AW.Services.SalesPerson.Application.GetSalesPersons;

namespace AW.Services.SalesPerson.WCF.Messages.ListSalesPersons
{
    public class SalesPersonPhone : IMapFrom<SalesPersonPhoneDto>
    {
        public string PhoneNumberType { get; set; }
        public string PhoneNumber { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SalesPersonPhoneDto, SalesPersonPhone>();
        }
    }
}