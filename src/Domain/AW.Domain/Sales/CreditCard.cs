using System;

namespace AW.Domain.Sales
{
    public partial class CreditCard
    {
        public virtual int Id { get; protected set; }

        public string CardType { get; set; }
        
        public string CardNumber { get; set; }

        public byte ExpMonth { get; set; }

        public short ExpYear { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}