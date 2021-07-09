using AW.Services.Product.Core.Entities;
using System.Data.Entity.ModelConfiguration;

namespace AW.Services.Product.Infrastructure.EF6.Configurations
{
    public class ProductDocumentConfiguration : EntityTypeConfiguration<ProductDocument>
    {
        public ProductDocumentConfiguration()
        {
            ToTable("ProductDocument");
            HasKey(pd => new { pd.ProductID, pd.DocumentNode });

            Property(pd => pd.ProductID)
                .HasColumnName("ProductID");
        }
    }
}