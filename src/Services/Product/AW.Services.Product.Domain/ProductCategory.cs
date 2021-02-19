using System.Collections.Generic;

namespace AW.Services.Product.Domain
{

    public class ProductCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<ProductSubcategory> ProductSubcategory { get; set; } = new List<ProductSubcategory>();
    }
}