using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.Product.Infrastructure.EFCore.Configurations
{
    public class ProductCategoryConfiguration : IEntityTypeConfiguration<Core.Entities.ProductCategory>
    {
        public void Configure(EntityTypeBuilder<Core.Entities.ProductCategory> builder)
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