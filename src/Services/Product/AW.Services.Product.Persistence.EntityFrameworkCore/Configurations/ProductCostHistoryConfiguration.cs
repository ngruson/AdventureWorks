using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.Product.Persistence.EntityFrameworkCore.Configurations
{
    public class ProductCostHistoryConfiguration : IEntityTypeConfiguration<Domain.ProductCostHistory>
    {
        public void Configure(EntityTypeBuilder<Domain.ProductCostHistory> builder)
        {
            builder.ToTable("ProductCostHistory");
            builder.HasKey(pch => new { pch.ProductID, pch.StartDate });

            builder.Property(pch => pch.StandardCost)
                .HasColumnType("decimal(19,4)")
                .HasColumnType("money");
        }
    }
}