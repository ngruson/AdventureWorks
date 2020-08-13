using AW.Domain;
using System;
using System.Collections.Generic;

namespace AW.Domain.Production
{

    public class ProductCategory : BaseEntity
    {
        public string Name { get; set; }

        public Guid rowguid { get; set; }

        public DateTime ModifiedDate { get; set; }

        public virtual ICollection<ProductSubcategory> ProductSubcategory { get; set; } = new List<ProductSubcategory>();
    }
}