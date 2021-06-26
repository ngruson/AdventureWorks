using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AW.Services.Product.Persistence.EntityFramework.Configurations
{
    public class ProductModelProductDescriptionCultureConfiguration : EntityTypeConfiguration<Domain.ProductModelProductDescriptionCulture>
    {
        public ProductModelProductDescriptionCultureConfiguration()
        {
            ToTable("ProductModelProductDescriptionCulture");
            HasKey(pmpdc => new { pmpdc.ProductModelID, pmpdc.ProductDescriptionID, pmpdc.CultureID });

            Property(pmpdc => pmpdc.ProductModelID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(pmpdc => pmpdc.ProductDescriptionID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(pmpdc => pmpdc.CultureID)
                .HasMaxLength(6);
        }
    }
}