using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.Product.Infrastructure.EFCore.Configurations
{
    public class ProductModelIllustrationConfiguration : IEntityTypeConfiguration<Core.Entities.ProductModelIllustration>
    {
        public void Configure(EntityTypeBuilder<Core.Entities.ProductModelIllustration> builder)
        {
            builder.ToTable("ProductModelIllustration");
            builder.HasKey(_ => new { _.ProductModelID, _.IllustrationID });

            builder.Property(_ => _.ProductModelID)
                .ValueGeneratedNever();

            builder.Property(_ => _.IllustrationID)
                .ValueGeneratedNever();
        }
    }
}