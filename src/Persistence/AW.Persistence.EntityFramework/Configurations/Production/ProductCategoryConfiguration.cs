using AW.Domain.Production;
using System.Data.Entity.ModelConfiguration;

namespace AW.Persistence.EntityFramework.Configurations.Production
{
    public class ProductCategoryConfiguration : EntityTypeConfiguration<ProductCategory>
    {
        public ProductCategoryConfiguration()
        {
            ToTable("Production.ProductCategory");

            Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(50);

            HasMany(pc => pc.ProductSubcategory)
                .WithRequired(psc => psc.ProductCategory)
                .WillCascadeOnDelete(false);
        }
    }
}