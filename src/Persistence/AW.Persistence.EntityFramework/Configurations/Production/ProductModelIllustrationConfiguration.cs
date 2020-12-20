using AW.Core.Domain.Production;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AW.Persistence.EntityFramework.Configurations.Production
{
    public class ProductModelIllustrationConfiguration : EntityTypeConfiguration<ProductModelIllustration>
    {
        public ProductModelIllustrationConfiguration()
        {
            ToTable("Production.ProductModelIllustration");
            HasKey(pmi => new { pmi.ProductModelID, pmi.IllustrationID });

            Property(pmi => pmi.ProductModelID)
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(pmi => pmi.IllustrationID)
                .HasColumnOrder(1)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        }
    }
}