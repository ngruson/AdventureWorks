using System;
using System.Collections.Generic;

namespace AW.Services.Product.Core.Entities
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string CatalogDescription { get; set; }

        public string Instructions { get; set; }

        public Guid rowguid { get; set; }

        public DateTime ModifiedDate { get; set; }

        public virtual ICollection<ProductModelIllustration> ProductModelIllustrations { get; set; } = new List<ProductModelIllustration>();

        public virtual ICollection<ProductModelProductDescriptionCulture> ProductModelProductDescriptionCultures { get; set; } = new List<ProductModelProductDescriptionCulture>();
    }
}