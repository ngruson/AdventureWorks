using AW.Core.Domain.Person;
using System.Collections.Generic;

namespace AW.Core.Abstractions.Api.CustomerApi.GetCustomer
{
    public class Person
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
    }
}