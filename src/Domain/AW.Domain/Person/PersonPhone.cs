using System;

namespace AW.Domain.Person
{
    public partial class PersonPhone : BaseEntity
    {
        public string PhoneNumber { get; set; }

        public int PhoneNumberTypeID { get; set; }

        public DateTime ModifiedDate { get; set; }

        public virtual Person Person { get; set; }

        public virtual PhoneNumberType PhoneNumberType { get; set; }
    }
}
