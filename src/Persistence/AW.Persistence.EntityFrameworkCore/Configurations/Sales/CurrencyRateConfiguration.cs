using AW.Core.Domain.Sales;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Persistence.EntityFrameworkCore.Configurations.Sales
{
    public class CurrencyRateConfiguration : IEntityTypeConfiguration<CurrencyRate>
    {
        public void Configure(EntityTypeBuilder<CurrencyRate> builder)
        {
            builder.ToTable("CurrencyRate", "Sales");

            builder.Property(cr => cr.FromCurrencyCode)
                .IsRequired()
                .HasMaxLength(3);

            builder.Property(cr => cr.ToCurrencyCode)
                .IsRequired()
                .HasMaxLength(3);

            builder.Property(cr => cr.AverageRate)
                .HasColumnType("money");

            builder.Property(cr => cr.EndOfDayRate)
                .HasColumnType("money");
        }
    }
}