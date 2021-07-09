using AW.Services.Product.Core.Entities;
using System.Data.Entity.ModelConfiguration;

namespace AW.Services.Product.Infrastructure.EF6.Configurations
{
    public class ProductInventoryConfiguration : EntityTypeConfiguration<ProductInventory>
    {
        public ProductInventoryConfiguration()
        {
            ToTable("ProductInventory");
            HasKey(pic => new { pic.ProductID, pic.LocationID });

            Property(pic => pic.Shelf)
                .IsRequired()
                .HasMaxLength(10);
        }
    }
}