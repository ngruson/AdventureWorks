using System.Collections.Generic;
using AW.Services.SharedKernel.Interfaces;

namespace AW.Services.Product.Core.Entities
{
    public class ProductSubcategory : IAggregateRoot
    {
        public int Id { get; set; }

        public string? Name { get; private set; }

        public ProductCategory? ProductCategory { get; private set; }

        public List<Product> Products { get; internal set; } = new();
    }
}
