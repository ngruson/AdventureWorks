using System;
using System.Collections.Generic;

namespace AW.Core.Domain.Production
{
    public class ProductDescription
    {
        public virtual int Id { get; protected set; }
        public string Description { get; set; }

        public Guid rowguid { get; set; }

        public DateTime ModifiedDate { get; set; }

        public virtual ICollection<ProductModelProductDescriptionCulture> ProductModelProductDescriptionCulture { get; set; } = new List<ProductModelProductDescriptionCulture>();
    }
}