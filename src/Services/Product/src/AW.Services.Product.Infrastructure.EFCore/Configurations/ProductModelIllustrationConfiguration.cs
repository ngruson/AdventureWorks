using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.Product.Infrastructure.EFCore.Configurations
{
    public class ProductModelIllustrationConfiguration : IEntityTypeConfiguration<Core.Entities.ProductModelIllustration>
    {
        public void Configure(EntityTypeBuilder<Core.Entities.ProductModelIllustration> builder)
        {
            builder.ToTable("ProductModelIllustration");
            builder.HasKey("ProductModelID", "IllustrationID");

            builder.Property("ProductModelID")
                .ValueGeneratedNever();

            builder.Property("IllustrationID")
                .ValueGeneratedNever();
        }
    }
}