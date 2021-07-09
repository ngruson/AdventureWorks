using AW.Services.Product.Core.Entities;
using System.Data.Entity.ModelConfiguration;

namespace AW.Services.Product.Infrastructure.EF6.Configurations
{
    public class ProductSubCategoryConfiguration : EntityTypeConfiguration<ProductSubcategory>
    {
        public ProductSubCategoryConfiguration()
        {
            ToTable("ProductSubcategory");

            Property(psc => psc.Id)
                .HasColumnName("ProductSubcategoryID");

            Property(psc => psc.Name)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}