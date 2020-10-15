using System;
using System.Collections.Generic;

namespace AW.Domain.Person
{
    public partial class BusinessEntity : BaseEntity
    {
        public Guid rowguid { get; set; }

        public DateTime ModifiedDate { get; set; }

        public virtual ICollection<BusinessEntityAddress> BusinessEntityAddresses { get; set; } = new List<BusinessEntityAddress>();

        public virtual ICollection<BusinessEntityContact> BusinessEntityContact { get; set; } = new List<BusinessEntityContact>();
    }
}