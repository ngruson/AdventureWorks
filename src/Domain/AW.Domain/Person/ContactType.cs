using System;

namespace AW.Domain.Person
{
    public partial class ContactType
    {
        public virtual int Id { get; protected set; }

        public string Name { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}