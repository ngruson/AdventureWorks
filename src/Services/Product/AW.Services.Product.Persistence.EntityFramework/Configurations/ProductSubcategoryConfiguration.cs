using System.Data.Entity.ModelConfiguration;

namespace AW.Services.Product.Persistence.EntityFramework.Configurations
{
    public class ProductSubCategoryConfiguration : EntityTypeConfiguration<Domain.ProductSubcategory>
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