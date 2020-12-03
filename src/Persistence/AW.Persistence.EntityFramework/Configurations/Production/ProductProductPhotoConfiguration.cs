using AW.Domain.Production;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AW.Persistence.EntityFramework.Configurations.Production
{
    public class ProductProductPhotoConfiguration : EntityTypeConfiguration<ProductProductPhoto>
    {
        public ProductProductPhotoConfiguration()
        {
            ToTable("Production.ProductProductPhoto");
            HasKey(ppp => new { ppp.ProductID, ppp.ProductPhotoID });

            Property(ppp => ppp.ProductID)
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(ppp => ppp.ProductPhotoID)
                .HasColumnOrder(1)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        }
    }
}