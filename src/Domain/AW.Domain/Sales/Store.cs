using AW.Domain.Person;
using System;
using System.Collections.Generic;

namespace AW.Domain.Sales
{
    public partial class Store
    {
        public int BusinessEntityID { get; set; }

        public string Name { get; set; }

        public int? SalesPersonID { get; set; }
        
        public string Demographics { get; set; }

        public Guid rowguid { get; set; }

        public DateTime ModifiedDate { get; set; }

        public virtual BusinessEntity BusinessEntity { get; set; }

        public virtual IEnumerable<Customer> Customer { get; set; }

        public virtual SalesPerson SalesPerson { get; set; }
    }
}