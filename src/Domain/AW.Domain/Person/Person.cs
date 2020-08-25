using AW.Domain.Sales;
using System.Collections.Generic;

namespace AW.Domain.Person
{
    public class Person : BusinessEntity
    {
        public string PersonType { get; set; }

        public bool NameStyle { get; set; }
        
        public string Title { get; set; }
        
        public string FirstName { get; set; }

        public string MiddleName { get; set; }
        
        public string LastName { get; set; }

        public string FullName => this.FullName();
        
        public string Suffix { get; set; }

        public EmailPromotion EmailPromotion { get; set; }

        public string AdditionalContactInfo { get; set; }
        
        public string Demographics { get; set; }

        public virtual ICollection<EmailAddress> EmailAddresses { get; set; } = new List<EmailAddress>();

        //public virtual Password Password { get; set; }

        public virtual ICollection<PersonCreditCard> CreditCards { get; set; } = new List<PersonCreditCard>();

        public virtual ICollection<PersonPhone> PhoneNumbers { get; set; }
    }
}