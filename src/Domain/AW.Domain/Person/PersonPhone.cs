using System;

namespace AW.Domain.Person
{
    public partial class PersonPhone
    {
        public int BusinessEntityID { get; set; }

        public string PhoneNumber { get; set; }

        public int PhoneNumberTypeID { get; set; }

        public DateTime ModifiedDate { get; set; }

        public virtual Person Person { get; set; }

        public virtual PhoneNumberType PhoneNumberType { get; set; }
    }
}
