using System;

namespace AW.Domain.Person
{    
    public class EmailAddress : BaseEntity
    {
        public int BusinessEntityID { get; set; }

        public int EmailAddressID { get; set; }

        public string EmailAddress1 { get; set; }

        public Guid rowguid { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}