using System.Data.Entity.ModelConfiguration;

namespace AW.Services.Product.Persistence.EntityFramework.Configurations
{
    public class ProductDocumentConfiguration : EntityTypeConfiguration<Domain.ProductDocument>
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