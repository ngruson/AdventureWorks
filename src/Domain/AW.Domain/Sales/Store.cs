using AW.Domain.Person;
using System;

namespace AW.Domain.Sales
{
    public partial class Store : BusinessEntity
    {
        public string Name { get; set; }
        
        public string Demographics { get; set; }

        public Guid rowguid { get; set; }

        public DateTime ModifiedDate { get; set; }

        public virtual SalesPerson SalesPerson { get; set; }
        public int? SalesPersonID { get; set; }
    }
}