using AW.Services.Product.Core.Entities;
using System.Data.Entity.ModelConfiguration;

namespace AW.Services.Product.Infrastructure.EF6.Configurations
{
    public class ProductCategoryConfiguration : EntityTypeConfiguration<ProductCategory>
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