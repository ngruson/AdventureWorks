using AW.Services.SharedKernel.Interfaces;

namespace AW.Services.Sales.Core.Entities
{
    public class SalesPerson : Person, IAggregateRoot
    {
        public string? Territory { get; set; }
        public List<SalesPersonEmailAddress> EmailAddresses { get; set; } = new();
        public List<SalesPersonPhone> PhoneNumbers { get; set; } = new();
    }
}