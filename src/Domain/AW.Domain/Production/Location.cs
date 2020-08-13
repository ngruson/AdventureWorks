using System;
using System.Collections.Generic;

namespace AW.Domain.Production
{
    public class Location : BaseEntity
    {
        public string Name { get; set; }

        public decimal CostRate { get; set; }

        public decimal Availability { get; set; }

        public DateTime ModifiedDate { get; set; }

        public virtual IEnumerable<ProductInventory> ProductInventory { get; set; } = new List<ProductInventory>();
    }
}