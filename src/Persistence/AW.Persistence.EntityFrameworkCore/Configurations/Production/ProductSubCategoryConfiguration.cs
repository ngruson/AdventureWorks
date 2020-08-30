using AW.Domain.Production;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Persistence.EntityFrameworkCore.Configurations.Production
{
    public class ProductSubCategoryConfiguration : IEntityTypeConfiguration<ProductSubcategory>
    {
        public void Configure(EntityTypeBuilder<ProductSubcategory> builder)
        {
            builder.ToTable("Production.ProductSubcategory");

            builder.Property(psc => psc.Id)
                .HasColumnName("ProductSubcategoryID");

            builder.Property(psc => psc.Name)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}