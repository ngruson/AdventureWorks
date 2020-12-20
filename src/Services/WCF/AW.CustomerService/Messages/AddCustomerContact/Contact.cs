using AutoMapper;
using AW.Core.Application.AutoMapper;
using AW.Core.Application.Customer.AddCustomerContact;
using System.Collections.Generic;

namespace AW.CustomerService.Messages.AddCustomerContact
{
    public class Contact : IMapFrom<ContactDto>
    {
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public List<EmailAddress> EmailAddresses { get; set; } = new List<EmailAddress>();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Contact, ContactDto>();
        }
    }
}