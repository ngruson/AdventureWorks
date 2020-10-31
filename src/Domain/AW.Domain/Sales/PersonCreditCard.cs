using System;

namespace AW.Domain.Sales
{    
    public class PersonCreditCard
    {
        public int BusinessEntityID { get; set; }
        public int CreditCardID { get; set; }

        public DateTime ModifiedDate { get; set; }

        public virtual CreditCard CreditCard { get; set; }
    }
}