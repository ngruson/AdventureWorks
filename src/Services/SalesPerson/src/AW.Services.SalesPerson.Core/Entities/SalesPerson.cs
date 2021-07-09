using AW.SharedKernel.Interfaces;
using System.Collections.Generic;

namespace AW.Services.SalesPerson.Core.Entities
{
    public class SalesPerson : IAggregateRoot, IPerson
    {
        public int Id { get; set; }
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