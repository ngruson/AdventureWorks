using AW.SharedKernel.Interfaces;

namespace AW.Services.Sales.Core.Entities
{
    public class CreditCard : IAggregateRoot
    {
        public int Id { get; set; }

        public string CardType { get; set; }

        public string CardNumber { get; set; }

        public byte ExpMonth { get; set; }

        public short ExpYear { get; set; }
    }
}