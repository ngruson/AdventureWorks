using AW.Domain.Person;
using System.Collections.Generic;

namespace AW.Domain.Sales
{
    public partial class Customer : BusinessEntity
    {
        public int? PersonID { get; set; }

        public int? StoreID { get; set; }

        public int? TerritoryID { get; set; }

        public string AccountNumber { get; set; }

        public virtual Person.Person Person { get; set; }

        public virtual SalesTerritory SalesTerritory { get; set; }

        public virtual Store Store { get; set; }

        public virtual ICollection<SalesOrderHeader> SalesOrders { get; set; } = new List<SalesOrderHeader>();
    }
}