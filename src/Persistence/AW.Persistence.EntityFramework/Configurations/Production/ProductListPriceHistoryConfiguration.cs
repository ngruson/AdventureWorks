using AW.Domain.Production;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AW.Persistence.EntityFramework.Configurations.Production
{
    public class ProductListPriceHistoryConfiguration : EntityTypeConfiguration<ProductListPriceHistory>
    {
        public ProductListPriceHistoryConfiguration()
        {
            ToTable("Production.ProductListPriceHistory");
            HasKey(plphc => new { plphc.ProductID, plphc.StartDate });

            Property(plphc => plphc.ProductID)
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(plphc => plphc.StartDate)
                .HasColumnOrder(1);

            Property(plphc => plphc.ListPrice)
                .HasPrecision(19, 4)
                .HasColumnType("money");
        }
    }
}