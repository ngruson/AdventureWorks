﻿using AutoMapper;
using AW.Common.AutoMapper;
using System.Collections.Generic;

namespace AW.Services.SalesPerson.Application.GetSalesPersons
{
    public class SalesPersonDto : IMapFrom<Domain.SalesPerson>
    {
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public string Territory { get; set; }
        public List<SalesPersonEmailAddressDto> EmailAddresses { get; set; }
        public List<SalesPersonPhoneDto> PhoneNumbers { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.SalesPerson, SalesPersonDto>();
        }
    }
}