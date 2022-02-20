using AW.SharedKernel.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace AW.Services.Product.Core.Entities
{

    public class ProductCategory : IAggregateRoot
    {
        private int Id { get; set; }
        public string Name { get; private set; }

        public IEnumerable<ProductSubcategory> ProductSubcategory => _productSubcategory.ToList();
        private List<ProductSubcategory> _productSubcategory = new();
    }
}