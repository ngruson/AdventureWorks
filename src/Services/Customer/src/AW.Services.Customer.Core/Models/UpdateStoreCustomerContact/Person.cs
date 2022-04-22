using AutoMapper;
using AW.Services.Customer.Core.Handlers.UpdateStoreCustomerContact;
using System.Collections.Generic;
using AW.SharedKernel.AutoMapper;
using AW.SharedKernel.ValueTypes;

namespace AW.Services.Customer.Core.Models.UpdateStoreCustomerContact
{
    public class Person : IMapFrom<PersonDto>
    {
        public string Title { get; set; }
        public NameFactory Name { get; set; }
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