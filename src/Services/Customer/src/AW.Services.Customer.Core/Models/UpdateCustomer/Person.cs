using AutoMapper;
using AW.SharedKernel.AutoMapper;
using AW.Services.Customer.Core.Handlers.UpdateCustomer;
using System.Collections.Generic;
using AW.Services.SharedKernel.ValueObjects;

namespace AW.Services.Customer.Core.Models.UpdateCustomer
{
    public class Person : IMapFrom<PersonDto>
    {
        public string Title { get; set; }
        public NameFactory Name { get; set; }
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