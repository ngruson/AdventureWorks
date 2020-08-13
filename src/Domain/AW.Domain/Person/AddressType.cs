using System;

namespace AW.Domain.Person
{
    public partial class AddressType
    {
        public int AddressTypeID { get; set; }
        
        public string Name { get; set; }

        public Guid rowguid { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}