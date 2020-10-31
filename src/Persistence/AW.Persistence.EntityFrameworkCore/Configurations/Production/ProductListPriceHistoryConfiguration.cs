using AW.Domain.Production;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Persistence.EntityFrameworkCore.Configurations.Production
{
    public class ProductListPriceHistoryConfiguration : IEntityTypeConfiguration<ProductListPriceHistory>
    {
        public void Configure(EntityTypeBuilder<ProductListPriceHistory> builder)
        {
            builder.ToTable("Production.ProductListPriceHistory");
            builder.HasKey(plphc => new { plphc.ProductID, plphc.StartDate });

            builder.Property(plphc => plphc.ProductID)
                .ValueGeneratedNever();

            builder.Property(plphc => plphc.ListPrice)
                .HasColumnType("decimal(19,4)")
                .HasColumnType("money");
        }
    }
}