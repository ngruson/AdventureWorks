using System.Collections.Generic;

namespace AW.Services.Product.Core.Entities
{
    public class ProductSubcategory
    {
        public int Id { get; set; }

        public string? Name { get; private set; }

        public ProductCategory? ProductCategory { get; private set; }

        public List<Product> Products { get; internal set; } = new();
    }
}