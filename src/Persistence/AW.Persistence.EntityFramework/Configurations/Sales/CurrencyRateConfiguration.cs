using AW.Core.Domain.Sales;
using System.Data.Entity.ModelConfiguration;

namespace AW.Persistence.EntityFramework.Configurations.Sales
{
    public class CurrencyRateConfiguration : EntityTypeConfiguration<CurrencyRate>
    {
        public CurrencyRateConfiguration()
        {
            ToTable("Sales.CurrencyRate");

            Property(cr => cr.FromCurrencyCode)
                .IsRequired()
                .HasMaxLength(3);

            Property(cr => cr.ToCurrencyCode)
                .IsRequired()
                .HasMaxLength(3);

            Property(cr => cr.AverageRate)
                .HasPrecision(19, 4)
                .HasColumnType("money");

            Property(cr => cr.EndOfDayRate)
                 .HasPrecision(19, 4)
                .HasColumnType("money");
        }
    }
}