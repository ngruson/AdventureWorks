using System;

namespace AW.Services.Product.Core.Entities
{
    public class ProductModelProductDescriptionCulture
    {
        private int ProductModelID { get; set; }
        private int ProductDescriptionID { get; set; }

        private string CultureID { get; set; }

        public DateTime ModifiedDate { get; private set; }

        public Culture Culture { get; private set; }

        public ProductDescription ProductDescription { get; private set; }
    }
}