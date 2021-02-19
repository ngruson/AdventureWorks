using System;
using System.Collections.Generic;

namespace AW.Core.Domain.Person
{
    public partial class BusinessEntity
    {
        public int Id { get; set; }
        public Guid rowguid { get; set; }

        public DateTime ModifiedDate { get; set; }

        public virtual ICollection<BusinessEntityAddress> BusinessEntityAddresses { get; set; } = new List<BusinessEntityAddress>();

        public virtual ICollection<BusinessEntityContact> BusinessEntityContacts { get; set; } = new List<BusinessEntityContact>();
    }
}