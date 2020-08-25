using System;

namespace AW.Domain.Person
{
    public partial class BusinessEntityAddress : BaseEntity
    {
        public int AddressID { get; set; }

        public int AddressTypeID { get; set; }

        public Guid rowguid { get; set; }

        public DateTime ModifiedDate { get; set; }

        public virtual Address Address { get; set; }

        public virtual AddressType AddressType { get; set; }
    }
}