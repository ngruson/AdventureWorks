using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.Product.Infrastructure.EFCore.Configurations
{
    public class ProductModelProductDescriptionCultureConfiguration : IEntityTypeConfiguration<Core.Entities.ProductModelProductDescriptionCulture>
    {
        public void Configure(EntityTypeBuilder<Core.Entities.ProductModelProductDescriptionCulture> builder)
        {
            builder.ToTable("ProductModelProductDescriptionCulture");
            builder.HasKey(pmpdc => new { pmpdc.ProductModelID, pmpdc.ProductDescriptionID, pmpdc.CultureID });

            builder.Property(pmpdc => pmpdc.ProductModelID)
                .ValueGeneratedNever();

            builder.Property(pmpdc => pmpdc.ProductDescriptionID)
                .ValueGeneratedNever();

            builder.Property(pmpdc => pmpdc.CultureID)
                .HasMaxLength(6);
        }
    }
}