using AW.Core.Domain.Production;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Persistence.EntityFrameworkCore.Configurations.Production
{
    public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.ToTable("Production.ProductCategory");

            builder.Property(c => c.Id)
                .HasColumnName("ProductCategoryID");

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasMany(pc => pc.ProductSubcategory)
                .WithOne(psc => psc.ProductCategory)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}