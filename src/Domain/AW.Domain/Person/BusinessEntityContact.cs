using System;

namespace AW.Domain.Person
{
    public partial class BusinessEntityContact
    {
        public int BusinessEntityID { get; set; }

        public int PersonID { get; set; }

        public int ContactTypeID { get; set; }

        public Guid rowguid { get; set; }

        public DateTime ModifiedDate { get; set; }

        public virtual ContactType ContactType { get; set; }

        public virtual Person Person { get; set; }
    }
}
