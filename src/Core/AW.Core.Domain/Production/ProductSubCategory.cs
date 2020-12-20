using System;
using System.Collections.Generic;

namespace AW.Core.Domain.Production
{
    public class ProductSubcategory
    {
        public virtual int Id { get; protected set; }
        public int ProductCategoryID { get; set; }

        public string Name { get; set; }

        public Guid rowguid { get; set; }

        public DateTime ModifiedDate { get; set; }

        public virtual IEnumerable<Product> Products { get; set; } = new List<Product>();

        public virtual ProductCategory ProductCategory { get; set; }
    }
}