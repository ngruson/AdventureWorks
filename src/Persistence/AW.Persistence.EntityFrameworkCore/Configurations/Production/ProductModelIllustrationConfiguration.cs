using AW.Domain.Production;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Persistence.EntityFrameworkCore.Configurations.Production
{
    public class ProductModelIllustrationConfiguration : IEntityTypeConfiguration<ProductModelIllustration>
    {
        public void Configure(EntityTypeBuilder<ProductModelIllustration> builder)
        {
            builder.ToTable("Production.ProductModelIllustration");
            builder.HasKey(pmi => new { pmi.Id, pmi.IllustrationID });

            builder.Property(pmi => pmi.Id)
                .ValueGeneratedNever();

            builder.Property(pmi => pmi.IllustrationID)
                .ValueGeneratedNever();
        }
    }
}