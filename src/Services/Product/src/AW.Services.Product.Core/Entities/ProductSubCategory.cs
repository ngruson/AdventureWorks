using System.Collections.Generic;
using System.Linq;

namespace AW.Services.Product.Core.Entities
{
    public class ProductSubcategory
    {
        private int Id { get; set; }
        //private int ProductCategoryId { get; set; }

        public string Name { get; private set; }

        public ProductCategory ProductCategory { get; private set; }

        public IEnumerable<Product> Products => _products.ToList();
        private List<Product> _products = new();
    }
}