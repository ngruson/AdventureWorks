using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.Product.Persistence.EntityFrameworkCore.Configurations
{
    public class ProductDocumentConfiguration : IEntityTypeConfiguration<Domain.ProductDocument>
    {
        public void Configure(EntityTypeBuilder<Domain.ProductDocument> builder)
        {
            builder.ToTable("ProductDocument");
            builder.HasKey(pd => new { pd.ProductID, pd.DocumentNode });

            builder.Property(pd => pd.ProductID)
                .HasColumnName("ProductID");
        }
    }
}