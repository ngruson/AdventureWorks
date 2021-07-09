using System.Collections.Generic;

namespace AW.Services.Product.Core.Entities
{
    public class ProductSubcategory
    {
        public int Id { get; set; }
        public int ProductCategoryId { get; set; }

        public string Name { get; set; }

        public ProductCategory ProductCategory { get; set; }

        public List<Product> Products { get; set; } = new List<Product>();
    }
}