using System;

namespace AW.Core.Domain.Person
{
    public partial class AddressType
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public Guid rowguid { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}