using AW.Common.Interfaces;
using System.Collections.Generic;

namespace AW.UI.Web.Common.ApiClients.SalesPersonApi.Models
{
    public class SalesPerson : IPerson
    {
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public string Territory { get; set; }
        public List<SalesPersonEmailAddress> EmailAddresses { get; set; }
        public List<SalesPersonPhone> PhoneNumbers { get; set; }
    }
}