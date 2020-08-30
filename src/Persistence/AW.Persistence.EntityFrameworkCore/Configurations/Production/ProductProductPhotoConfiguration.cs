using AW.Domain.Production;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Persistence.EntityFrameworkCore.Configurations.Production
{
    public class ProductProductPhotoConfiguration : IEntityTypeConfiguration<ProductProductPhoto>
    {
        public void Configure(EntityTypeBuilder<ProductProductPhoto> builder)
        {
            builder.ToTable("Production.ProductProductPhoto");
            builder.HasKey(ppp => new { ppp.ProductID, ppp.ProductPhotoID });

            builder.Property(ppp => ppp.ProductID)
                .ValueGeneratedNever();

            builder.Property(ppp => ppp.ProductPhotoID)
                .ValueGeneratedNever();
        }
    }
}