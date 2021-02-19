using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.Product.Persistence.EntityFrameworkCore.Configurations
{
    public class ProductModelProductDescriptionCultureConfiguration : IEntityTypeConfiguration<Domain.ProductModelProductDescriptionCulture>
    {
        public void Configure(EntityTypeBuilder<Domain.ProductModelProductDescriptionCulture> builder)
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