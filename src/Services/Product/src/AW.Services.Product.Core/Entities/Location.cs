using System;
using System.Collections.Generic;
using System.Linq;

namespace AW.Services.Product.Core.Entities
{
    public class Location
    {
        public int Id { get; set; }
        public string Name { get; private set; }

        public decimal CostRate { get; private set; }

        public decimal Availability { get; private set; }

        public DateTime ModifiedDate { get; private set; }

        public List<ProductInventory> ProductInventory { get; internal set; } = new();
    }
}