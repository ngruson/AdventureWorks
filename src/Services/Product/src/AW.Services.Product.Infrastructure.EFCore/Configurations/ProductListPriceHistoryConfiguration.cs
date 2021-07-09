using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.Product.Infrastructure.EFCore.Configurations
{
    public class ProductListPriceHistoryConfiguration : IEntityTypeConfiguration<Core.Entities.ProductListPriceHistory>
    {
        public void Configure(EntityTypeBuilder<Core.Entities.ProductListPriceHistory> builder)
        {
            builder.ToTable("ProductListPriceHistory");
            builder.HasKey(plphc => new { plphc.ProductID, plphc.StartDate });

            builder.Property(plphc => plphc.ProductID)
                .ValueGeneratedNever();

            builder.Property(plphc => plphc.ListPrice)
                .HasColumnType("decimal(19,4)")
                .HasColumnType("money");
        }
    }
}