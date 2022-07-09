using System.Collections.Generic;
using System.Linq;

namespace AW.Services.Product.Core.Entities
{
    public class ProductSubcategory
    {
        public int Id { get; set; }

        public string Name { get; private set; }

        public ProductCategory ProductCategory { get; private set; }

        public IEnumerable<Product> Products => _products.ToList();
        private readonly List<Product> _products = new();
    }
}