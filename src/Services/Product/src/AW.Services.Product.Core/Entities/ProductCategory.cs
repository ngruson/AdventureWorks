using AW.SharedKernel.Interfaces;
using System.Collections.Generic;

namespace AW.Services.Product.Core.Entities
{

    public class ProductCategory : IAggregateRoot
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<ProductSubcategory> ProductSubcategory { get; set; } = new List<ProductSubcategory>();
    }
}