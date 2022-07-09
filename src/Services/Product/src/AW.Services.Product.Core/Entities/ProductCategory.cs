using AW.Services.SharedKernel.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace AW.Services.Product.Core.Entities
{

    public class ProductCategory : IAggregateRoot
    {
        public int Id { get; set; }
        public string Name { get; private set; }

        public IEnumerable<ProductSubcategory> ProductSubcategory => _productSubcategory.ToList();
        private readonly List<ProductSubcategory> _productSubcategory = new();
    }
}