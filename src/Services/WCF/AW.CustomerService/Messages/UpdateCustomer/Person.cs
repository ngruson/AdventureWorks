using AutoMapper;
using AW.Application.AutoMapper;
using AW.Application.Customer.GetCustomer;
using System.Collections.Generic;

namespace AW.CustomerService.Messages.UpdateCustomer
{
    public class Person : IMapFrom<PersonCustomerDto>
    {
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Suffix { get; set; }
        public EmailPromotion EmailPromotion { get; set; }
        public List<CustomerAddress> Addresses { get; set; }
        public List<ContactInfo> ContactInfo { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PersonCustomerDto, Person>();
        }
    }
}