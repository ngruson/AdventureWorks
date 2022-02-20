using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.Product.Infrastructure.EFCore.Configurations
{
    public class ProductProductPhotoConfiguration : IEntityTypeConfiguration<Core.Entities.ProductProductPhoto>
    {
        public void Configure(EntityTypeBuilder<Core.Entities.ProductProductPhoto> builder)
        {
            builder.ToTable("ProductProductPhoto");
            builder.HasKey("ProductId", "ProductPhotoId");

            builder.Property("ProductId")
                .ValueGeneratedNever();

            builder.Property("ProductPhotoId")
                .ValueGeneratedNever();
        }
    }
}