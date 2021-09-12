﻿using AutoMapper;
using AW.Services.Customer.Core.Handlers.DeleteStoreCustomerContact;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Customer.Core.Models.DeleteStoreCustomerContact
{
    public class Person : IMapFrom<PersonDto>
    {
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Person, PersonDto>();
        }
    }
}