using AW.Services.Product.Core.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AW.Services.Product.Infrastructure.EF6.Configurations
{
    public class ProductModelIllustrationConfiguration : EntityTypeConfiguration<ProductModelIllustration>
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