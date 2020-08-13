using AW.Domain.Production;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AW.Persistence.EntityFramework.Configurations.Production
{
    public class ProductModelProductDescriptionCultureConfiguration : EntityTypeConfiguration<ProductModelProductDescriptionCulture>
    {
        public ProductModelProductDescriptionCultureConfiguration()
        {
            ToTable("Production.ProductModelProductDescriptionCulture");
            HasKey(pmpdc => new { pmpdc.Id, pmpdc.ProductDescriptionID, pmpdc.CultureID });

            Property(pmpdc => pmpdc.Id)
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(pmpdc => pmpdc.ProductDescriptionID)
                .HasColumnOrder(1)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(pmpdc => pmpdc.CultureID)
                .HasMaxLength(6)
                .HasColumnOrder(2);        
        }
    }
}