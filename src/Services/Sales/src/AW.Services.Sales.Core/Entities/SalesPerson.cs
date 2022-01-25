using AW.SharedKernel.Interfaces;
using System.Collections.Generic;

namespace AW.Services.Sales.Core.Entities
{
    public class SalesPerson : Person, IAggregateRoot
    {
        public string Territory { get; set; }
        public List<SalesPersonEmailAddress> EmailAddresses { get; set; }
        public List<SalesPersonPhone> PhoneNumbers { get; set; }
    }
}