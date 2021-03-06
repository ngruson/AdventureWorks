﻿using AutoMapper;
using AW.Services.Customer.Application.Common;
using System;
using System.Collections.Generic;

namespace AW.Services.Customer.Application.AddStoreCustomerContact
{
    public class ContactDto : IMapFrom<Domain.Person>
    {
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public List<EmailAddressDto> EmailAddresses { get; set; } = new List<EmailAddressDto>();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ContactDto, Domain.Person>();
        }
    }
}