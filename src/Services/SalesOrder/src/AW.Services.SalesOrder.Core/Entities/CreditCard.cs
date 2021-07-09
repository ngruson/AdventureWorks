namespace AW.Services.SalesOrder.Core.Entities
{
    public class CreditCard
    {
        public int Id { get; set; }

        public string CardType { get; set; }

        public string CardNumber { get; set; }

        public byte ExpMonth { get; set; }

        public short ExpYear { get; set; }
    }
}