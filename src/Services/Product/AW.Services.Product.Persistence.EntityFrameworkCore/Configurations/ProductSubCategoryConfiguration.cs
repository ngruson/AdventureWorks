using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.Product.Persistence.EntityFrameworkCore.Configurations
{
    public class ProductSubCategoryConfiguration : IEntityTypeConfiguration<Domain.ProductSubcategory>
    {
        public void Configure(EntityTypeBuilder<Domain.ProductSubcategory> builder)
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