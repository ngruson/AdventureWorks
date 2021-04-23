using AutoMapper;
using AW.Services.Customer.Application.Common;
using AW.Services.Customer.Application.UpdateCustomer;
using System.Collections.Generic;

namespace AW.Services.Customer.REST.API.Models.UpdateCustomer
{
    public class Person : IMapFrom<PersonDto>
    {
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public List<PersonEmailAddress> EmailAddresses { get; set; }
        public List<PersonPhone> PhoneNumbers { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Person, PersonDto>()
                .ReverseMap();
        }
    }
}