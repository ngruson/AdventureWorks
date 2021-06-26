using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AW.Services.Product.Persistence.EntityFramework.Configurations
{
    public class ProductModelIllustrationConfiguration : EntityTypeConfiguration<Domain.ProductModelIllustration>
    {
        public ProductModelIllustrationConfiguration()
        {
            ToTable("ProductModelIllustration");
            HasKey(pmi => new { pmi.ProductModelID, pmi.IllustrationID });

            Property(pmi => pmi.ProductModelID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(pmi => pmi.IllustrationID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        }
    }
}