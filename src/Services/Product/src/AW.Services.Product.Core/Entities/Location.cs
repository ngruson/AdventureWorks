using AW.Services.SharedKernel.Interfaces;

namespace AW.Services.Product.Core.Entities
{
    public class Location : IAggregateRoot
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public decimal CostRate { get; set; }

        public decimal Availability { get; set; }

        public List<ProductInventory> ProductInventory { get; internal set; } = new();
    }
}
