using AW.Services.SharedKernel.Interfaces;

namespace AW.Services.Product.Core.Entities
{
    public class ProductSubcategory : IAggregateRoot
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public ProductCategory? ProductCategory { get; set; }

        public List<Product> Products { get; internal set; } = new();
    }
}
