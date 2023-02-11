using AW.Services.SharedKernel.Interfaces;
using System.Collections.Generic;

namespace AW.Services.Product.Core.Entities
{

    public class ProductCategory : IAggregateRoot
    {
        public int Id { get; set; }
        public string? Name { get; private set; }

        public List<ProductSubcategory> ProductSubcategory { get; internal set; } = new();
    }
}