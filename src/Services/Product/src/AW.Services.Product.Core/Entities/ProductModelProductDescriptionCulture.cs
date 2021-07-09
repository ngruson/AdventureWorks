using System;

namespace AW.Services.Product.Core.Entities
{
    public class ProductModelProductDescriptionCulture
    {
        public int ProductModelID { get; set; }
        public int ProductDescriptionID { get; set; }

        public string CultureID { get; set; }

        public DateTime ModifiedDate { get; set; }

        public virtual Culture Culture { get; set; }

        public virtual ProductDescription ProductDescription { get; set; }
    }
}