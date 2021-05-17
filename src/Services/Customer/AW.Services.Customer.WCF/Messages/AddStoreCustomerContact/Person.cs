﻿using AutoMapper;
using AW.Common.AutoMapper;
using AW.Services.Customer.Application.AddStoreCustomerContact;
using System.Collections.Generic;

namespace AW.Services.Customer.WCF.Messages.AddStoreCustomerContact
{
    public class Person : IMapFrom<PersonDto>
    {
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public List<PersonEmailAddress> EmailAddresses { get; set; } = new List<PersonEmailAddress>();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Person, PersonDto>();
        }
    }
}