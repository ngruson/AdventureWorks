using AW.Core.Domain.Production;
using System.Data.Entity.ModelConfiguration;

namespace AW.Persistence.EntityFramework.Configurations.Production
{
    public class ProductSubCategoryConfiguration : EntityTypeConfiguration<ProductSubcategory>
    {
        public ProductSubCategoryConfiguration()
        {
            ToTable("Production.ProductSubcategory");

            Property(psc => psc.Id)
                .HasColumnName("ProductSubcategoryID");

            Property(psc => psc.Name)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}