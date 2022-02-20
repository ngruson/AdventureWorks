using System;
using System.Collections.Generic;
using System.Linq;

namespace AW.Services.Product.Core.Entities
{
    public class ProductModel
    {
        private int Id { get; set; }
        public string Name { get; private set; }

        public string CatalogDescription { get; private set; }

        public string Instructions { get; private set; }

        public Guid rowguid { get; private set; }

        public DateTime ModifiedDate { get; private set; }

        public IEnumerable<ProductModelIllustration> ProductModelIllustrations => _productModelIllustrations.ToList();
        private List<ProductModelIllustration> _productModelIllustrations = new();

        public IEnumerable<ProductModelProductDescriptionCulture> ProductModelProductDescriptionCultures => _productModelProductDescriptionCultures.ToList();
        private List<ProductModelProductDescriptionCulture> _productModelProductDescriptionCultures = new();
    }
}