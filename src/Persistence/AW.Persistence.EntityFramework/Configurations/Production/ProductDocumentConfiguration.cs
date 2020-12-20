using AW.Core.Domain.Production;
using System.Data.Entity.ModelConfiguration;

namespace AW.Persistence.EntityFramework.Configurations.Production
{
    public class ProductDocumentConfiguration : EntityTypeConfiguration<ProductDocument>
    {
        public ProductDocumentConfiguration()
        {
            ToTable("Production.ProductDocument");
            HasKey(pd => new { pd.ProductID, pd.DocumentNode });
        }
    }
}