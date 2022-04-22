using AW.SharedKernel.ValueTypes;
using System.Collections.Generic;

namespace AW.UI.Web.Infrastructure.ApiClients.SalesPersonApi.Models
{
    public class SalesPerson
    {
        public string Title { get; set; }
        public NameFactory Name { get; set; }
        public string Suffix { get; set; }
        public string Territory { get; set; }
        public List<SalesPersonEmailAddress> EmailAddresses { get; set; }
        public List<SalesPersonPhone> PhoneNumbers { get; set; }
    }
}