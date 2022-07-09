using System;
using System.Collections.Generic;
using System.Linq;

namespace AW.Services.Product.Core.Entities
{
    public class ProductDescription
    {
        public int Id { get; set; }
        
        public string Description { get; private set; }

        public Guid Rowguid { get; private set; }

        public DateTime ModifiedDate { get; private set; }

        public IEnumerable<ProductModelProductDescriptionCulture> ProductModelProductDescriptionCulture => _productModelProductDescriptionCulture.ToList();
        private readonly List<ProductModelProductDescriptionCulture> _productModelProductDescriptionCulture = new();
    }
}