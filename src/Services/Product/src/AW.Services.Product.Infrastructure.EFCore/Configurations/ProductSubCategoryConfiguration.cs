using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.Product.Infrastructure.EFCore.Configurations
{
    public class ProductSubCategoryConfiguration : IEntityTypeConfiguration<Core.Entities.ProductSubcategory>
    {
        public void Configure(EntityTypeBuilder<Core.Entities.ProductSubcategory> builder)
        {
            builder.ToTable("ProductSubcategory");

            builder.Property(psc => psc.Id)
                .HasColumnName("ProductSubcategoryID");

            builder.Property(psc => psc.Name)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}