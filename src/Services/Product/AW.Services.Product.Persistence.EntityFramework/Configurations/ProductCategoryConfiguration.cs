using System.Data.Entity.ModelConfiguration;

namespace AW.Services.Product.Persistence.EntityFramework.Configurations
{
    public class ProductCategoryConfiguration : EntityTypeConfiguration<Domain.ProductCategory>
    {
        public ProductCategoryConfiguration()
        {
            ToTable("ProductCategory");

            Property(c => c.Id)
                .HasColumnName("ProductCategoryID");

            Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(50);

            HasMany(pc => pc.ProductSubcategory)
                .WithRequired(psc => psc.ProductCategory)
                .WillCascadeOnDelete(false);
        }
    }
}