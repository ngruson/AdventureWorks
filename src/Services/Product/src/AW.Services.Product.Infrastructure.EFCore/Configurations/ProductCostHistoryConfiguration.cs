using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.Product.Infrastructure.EFCore.Configurations
{
    public class ProductCostHistoryConfiguration : IEntityTypeConfiguration<Core.Entities.ProductCostHistory>
    {
        public void Configure(EntityTypeBuilder<Core.Entities.ProductCostHistory> builder)
        {
            builder.ToTable("ProductCostHistory");
            builder.HasKey(pch => new { pch.ProductID, pch.StartDate });

            builder.Property(pch => pch.StandardCost)
                .HasColumnType("decimal(19,4)")
                .HasColumnType("money");
        }
    }
}