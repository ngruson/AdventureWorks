using AW.Domain.Sales;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Persistence.EntityFrameworkCore.Configurations.Sales
{
    public class CountryRegionCurrencyConfiguration : IEntityTypeConfiguration<CountryRegionCurrency>
    {
        public void Configure(EntityTypeBuilder<CountryRegionCurrency> builder)
        {
            builder.ToTable("CountryRegionCurrency", "Sales");
            builder.HasKey(crc => new { crc.CountryRegionCode, crc.CurrencyCode });

            builder.Property(crc => crc.CountryRegionCode)
                .HasMaxLength(3);

            builder.Property(crc => crc.CurrencyCode)
                .HasMaxLength(3);
        }
    }
}