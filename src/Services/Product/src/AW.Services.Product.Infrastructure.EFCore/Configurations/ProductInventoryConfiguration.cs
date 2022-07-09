using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.Product.Infrastructure.EFCore.Configurations
{
    public class ProductInventoryConfiguration : IEntityTypeConfiguration<Core.Entities.ProductInventory>
    {
        public void Configure(EntityTypeBuilder<Core.Entities.ProductInventory> builder)
        {
            builder.ToTable("ProductInventory");
            builder.HasKey(_ => new { _.ProductID, _.LocationID });

            builder.Property(pic => pic.Shelf)
                .IsRequired()
                .HasMaxLength(10);
        }
    }
}