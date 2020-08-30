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
            builder.HasKey(plphc => new { plphc.Id, plphc.StartDate });

            builder.Property(plphc => plphc.Id)
                .ValueGeneratedNever();

            builder.Property(plphc => plphc.ListPrice)
                .HasColumnType("decimal(19,4)")
                .HasColumnType("money");
        }
    }
}