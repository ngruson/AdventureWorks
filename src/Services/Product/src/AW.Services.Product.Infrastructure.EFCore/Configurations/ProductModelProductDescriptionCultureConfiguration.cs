using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.Product.Infrastructure.EFCore.Configurations
{
    public class ProductModelProductDescriptionCultureConfiguration : IEntityTypeConfiguration<Core.Entities.ProductModelProductDescriptionCulture>
    {
        public void Configure(EntityTypeBuilder<Core.Entities.ProductModelProductDescriptionCulture> builder)
        {
            builder.ToTable("ProductModelProductDescriptionCulture");
            builder.HasKey("ProductModelID", "ProductDescriptionID", "CultureID");

            builder.Property("ProductModelID")
                .ValueGeneratedNever();

            builder.Property("ProductDescriptionID")
                .ValueGeneratedNever();

            builder.Property("CultureID")
                .HasMaxLength(6);
        }
    }
}