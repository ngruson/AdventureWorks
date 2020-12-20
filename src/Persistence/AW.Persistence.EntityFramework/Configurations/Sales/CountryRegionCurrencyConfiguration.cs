using AW.Core.Domain.Sales;
using System.Data.Entity.ModelConfiguration;

namespace AW.Persistence.EntityFramework.Configurations.Sales
{
    public class CountryRegionCurrencyConfiguration : EntityTypeConfiguration<CountryRegionCurrency>
    {
        public CountryRegionCurrencyConfiguration()
        {
            ToTable("Sales.CountryRegionCurrency");
            HasKey(crc => new { crc.CountryRegionCode, crc.CurrencyCode });

            Property(crc => crc.CountryRegionCode)
                .HasColumnOrder(0)
                .HasMaxLength(3);

            Property(crc => crc.CurrencyCode)
                .HasColumnOrder(1)
                .HasMaxLength(3);
        }
    }
}