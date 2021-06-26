using System.Data.Entity.ModelConfiguration;

namespace AW.Services.Product.Persistence.EntityFramework.Configurations
{
    public class ProductCostHistoryConfiguration : EntityTypeConfiguration<Domain.ProductCostHistory>
    {
        public ProductCostHistoryConfiguration()
        {
            ToTable("ProductCostHistory");
            HasKey(pch => new { pch.ProductID, pch.StartDate });

            Property(pch => pch.StandardCost)
                .HasColumnType("decimal(19,4)")
                .HasColumnType("money");
        }
    }
}