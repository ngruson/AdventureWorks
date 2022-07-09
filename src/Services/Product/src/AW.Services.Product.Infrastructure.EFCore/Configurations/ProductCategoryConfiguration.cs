using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.Product.Infrastructure.EFCore.Configurations
{
    public class ProductCategoryConfiguration : IEntityTypeConfiguration<Core.Entities.ProductCategory>
    {
        public void Configure(EntityTypeBuilder<Core.Entities.ProductCategory> builder)
        {
            builder.ToTable("ProductCategory");
            builder.HasKey(_ => _.Id);

            builder.Property(_ => _.Id)
                .HasColumnName("ProductCategoryID");

            builder.Property(_ => _.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasMany(_ => _.ProductSubcategory)
                .WithOne(_ => _.ProductCategory)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}