using System.Collections.Generic;

namespace AW.Core.Abstractions.Api.CustomerApi.AddCustomerContact
{
    public class Contact
    {
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public List<EmailAddress> EmailAddresses { get; set; } = new List<EmailAddress>();
    }
}