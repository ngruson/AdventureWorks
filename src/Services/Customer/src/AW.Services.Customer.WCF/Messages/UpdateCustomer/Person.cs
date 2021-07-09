using AutoMapper;
using AW.SharedKernel.AutoMapper;
using AW.Services.Customer.Core.Handlers.UpdateCustomer;
using System.Collections.Generic;

namespace AW.Services.Customer.WCF.Messages.UpdateCustomer
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