using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.Product.Infrastructure.EFCore.Configurations
{
    public class ProductModelProductDescriptionCultureConfiguration : IEntityTypeConfiguration<Core.Entities.ProductModelProductDescriptionCulture>
    {
        public void Configure(EntityTypeBuilder<Core.Entities.ProductModelProductDescriptionCulture> builder)
        {
            builder.ToTable("ProductModelProductDescriptionCulture");
            builder.HasKey(_ => new { _.ProductModelID, _.ProductDescriptionID, _.CultureID });

            builder.Property(_ => _.ProductModelID)
                .ValueGeneratedNever();

            builder.Property(_ => _.ProductDescriptionID)
                .ValueGeneratedNever();

            builder.Property(_ => _.CultureID)
                .HasMaxLength(6);
        }
    }
}