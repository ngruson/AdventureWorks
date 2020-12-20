using AW.Core.Domain.Production;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Persistence.EntityFrameworkCore.Configurations.Production
{
    public class ProductModelProductDescriptionCultureConfiguration : IEntityTypeConfiguration<ProductModelProductDescriptionCulture>
    {
        public void Configure(EntityTypeBuilder<ProductModelProductDescriptionCulture> builder)
        {
            builder.ToTable("Production.ProductModelProductDescriptionCulture");
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