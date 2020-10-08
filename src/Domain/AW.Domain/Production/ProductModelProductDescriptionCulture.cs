using System;

namespace AW.Domain.Production
{
    public class ProductModelProductDescriptionCulture : BaseEntity
    {
        public int ProductDescriptionID { get; set; }

        public string CultureID { get; set; }

        public DateTime ModifiedDate { get; set; }

        public virtual Culture Culture { get; set; }

        public virtual ProductDescription ProductDescription { get; set; }
    }
}