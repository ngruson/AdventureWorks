using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AW.Services.Product.Persistence.EntityFramework.Configurations
{
    public class ProductListPriceHistoryConfiguration : EntityTypeConfiguration<Domain.ProductListPriceHistory>
    {
        public ProductListPriceHistoryConfiguration()
        {
            ToTable("ProductListPriceHistory");
            HasKey(plphc => new { plphc.ProductID, plphc.StartDate });

            Property(plphc => plphc.ProductID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(plphc => plphc.ListPrice)
                .HasColumnType("decimal(19,4)")
                .HasColumnType("money");
        }
    }
}