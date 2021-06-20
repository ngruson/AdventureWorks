﻿using AutoMapper;
using AW.Common.AutoMapper;
using AW.Services.SalesPerson.Application.GetSalesPerson;
using System.Collections.Generic;

namespace AW.Services.SalesPerson.WCF.Messages.GetSalesPerson
{
    public class SalesPerson : IMapFrom<SalesPersonDto>
    {
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public string Territory { get; set; }
        public List<SalesPersonEmailAddress> EmailAddresses { get; set; }
        public List<SalesPersonPhone> PhoneNumbers { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SalesPersonDto, SalesPerson>();
        }
    }
}