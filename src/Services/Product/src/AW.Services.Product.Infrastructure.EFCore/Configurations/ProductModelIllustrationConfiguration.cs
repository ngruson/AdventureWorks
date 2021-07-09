using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.Product.Infrastructure.EFCore.Configurations
{
    public class ProductModelIllustrationConfiguration : IEntityTypeConfiguration<Core.Entities.ProductModelIllustration>
    {
        public void Configure(EntityTypeBuilder<Core.Entities.ProductModelIllustration> builder)
        {
            builder.ToTable("ProductModelIllustration");
            builder.HasKey(pmi => new { pmi.ProductModelID, pmi.IllustrationID });

            builder.Property(pmi => pmi.ProductModelID)
                .ValueGeneratedNever();

            builder.Property(pmi => pmi.IllustrationID)
                .ValueGeneratedNever();
        }
    }
}