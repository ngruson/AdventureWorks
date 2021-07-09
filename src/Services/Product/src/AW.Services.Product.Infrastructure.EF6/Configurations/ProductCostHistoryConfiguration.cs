using AW.Services.Product.Core.Entities;
using System.Data.Entity.ModelConfiguration;

namespace AW.Services.Product.Infrastructure.EF6.Configurations
{
    public class ProductCostHistoryConfiguration : EntityTypeConfiguration<ProductCostHistory>
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