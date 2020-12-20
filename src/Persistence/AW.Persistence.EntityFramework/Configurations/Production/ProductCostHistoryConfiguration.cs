using AW.Core.Domain.Production;
using System.Data.Entity.ModelConfiguration;

namespace AW.Persistence.EntityFramework.Configurations.Production
{
    public class ProductCostHistoryConfiguration : EntityTypeConfiguration<ProductCostHistory>
    {
        public ProductCostHistoryConfiguration()
        {
            ToTable("Production.ProductCostHistory");

            HasKey(pch => new { pch.ProductID, pch.StartDate });

            Property(pch => pch.StandardCost)
                .HasPrecision(19, 4)
                .HasColumnType("money");
        }
    }
}