using AW.Domain.Production;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Persistence.EntityFrameworkCore.Configurations.Production
{
    public class ProductInventoryConfiguration : IEntityTypeConfiguration<ProductInventory>
    {
        public void Configure(EntityTypeBuilder<ProductInventory> builder)
        {
            builder.ToTable("Production.ProductInventory");
            builder.HasKey(pic => new { pic.ProductID, pic.LocationID });

            builder.Property(pic => pic.Shelf)
                .IsRequired()
                .HasMaxLength(10);
        }
    }
}