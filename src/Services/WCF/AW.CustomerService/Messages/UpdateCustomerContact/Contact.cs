using AutoMapper;
using AW.Application.AutoMapper;
using AW.Application.Customer.UpdateCustomerContact;
using System.Collections.Generic;

namespace AW.CustomerService.Messages.UpdateCustomerContact
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