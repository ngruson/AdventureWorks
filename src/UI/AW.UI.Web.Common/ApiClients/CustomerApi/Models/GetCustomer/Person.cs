using AW.Common.Interfaces;
using System.Collections.Generic;

namespace AW.UI.Web.Common.ApiClients.CustomerApi.Models.GetCustomer
{
    public class Person : IPerson
    {
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public List<PersonEmailAddress> EmailAddresses { get; set; }
        public List<PersonPhone> PhoneNumbers { get; set; }
    }
}