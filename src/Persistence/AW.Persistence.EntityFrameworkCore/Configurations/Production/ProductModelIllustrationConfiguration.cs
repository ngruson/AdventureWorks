using AW.Core.Domain.Production;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Persistence.EntityFrameworkCore.Configurations.Production
{
    public class ProductModelIllustrationConfiguration : IEntityTypeConfiguration<ProductModelIllustration>
    {
        public void Configure(EntityTypeBuilder<ProductModelIllustration> builder)
        {
            builder.ToTable("Production.ProductModelIllustration");
            builder.HasKey(pmi => new { pmi.ProductModelID, pmi.IllustrationID });

            builder.Property(pmi => pmi.ProductModelID)
                .ValueGeneratedNever();

            builder.Property(pmi => pmi.IllustrationID)
                .ValueGeneratedNever();
        }
    }
}