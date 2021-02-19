using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.Product.Persistence.EntityFrameworkCore.Configurations
{
    public class ProductModelIllustrationConfiguration : IEntityTypeConfiguration<Domain.ProductModelIllustration>
    {
        public void Configure(EntityTypeBuilder<Domain.ProductModelIllustration> builder)
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