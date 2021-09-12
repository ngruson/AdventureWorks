﻿using AutoMapper;
using AW.SharedKernel.AutoMapper;
using AW.Services.Customer.Core.Handlers.GetCustomers;
using System.Collections.Generic;
using AW.Services.Customer.Core.Handlers.GetCustomer;

namespace AW.Services.Customer.Core.Models.GetCustomers
{
    public class Person : IMapFrom<PersonDto>
    {
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public List<PersonEmailAddressDto> EmailAddresses { get; set; }
        public List<PersonPhoneDto> PhoneNumbers { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PersonDto, Person>();
        }
    }
}