using AutoMapper;
using AW.Services.Customer.Core.Handlers.UpdateStoreCustomerContact;
using System.Collections.Generic;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Customer.WCF.Messages.UpdateStoreCustomerContact
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