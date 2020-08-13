using System;

namespace AW.Domain.Person
{
    public partial class ContactType
    {
        public int ContactTypeID { get; set; }
        
        public string Name { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}