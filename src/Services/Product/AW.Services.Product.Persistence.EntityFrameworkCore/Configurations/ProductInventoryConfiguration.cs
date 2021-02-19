using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.Product.Persistence.EntityFrameworkCore.Configurations
{
    public class ProductInventoryConfiguration : IEntityTypeConfiguration<Domain.ProductInventory>
    {
        public void Configure(EntityTypeBuilder<Domain.ProductInventory> builder)
        {
            builder.ToTable("ProductInventory");
            builder.HasKey(pic => new { pic.ProductID, pic.LocationID });

            builder.Property(pic => pic.Shelf)
                .IsRequired()
                .HasMaxLength(10);
        }
    }
}