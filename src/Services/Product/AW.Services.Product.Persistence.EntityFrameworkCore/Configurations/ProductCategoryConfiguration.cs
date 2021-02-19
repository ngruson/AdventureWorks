using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.Product.Persistence.EntityFrameworkCore.Configurations
{
    public class ProductCategoryConfiguration : IEntityTypeConfiguration<Domain.ProductCategory>
    {
        public void Configure(EntityTypeBuilder<Domain.ProductCategory> builder)
        {
            builder.ToTable("ProductCategory");

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