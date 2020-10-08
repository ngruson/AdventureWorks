using System;
using System.Collections.Generic;

namespace AW.Domain.Production
{
    public class ProductDescription : BaseEntity
    {
        public string Description { get; set; }

        public Guid rowguid { get; set; }

        public DateTime ModifiedDate { get; set; }

        public virtual ICollection<ProductModelProductDescriptionCulture> ProductModelProductDescriptionCulture { get; set; } = new List<ProductModelProductDescriptionCulture>();
    }
}