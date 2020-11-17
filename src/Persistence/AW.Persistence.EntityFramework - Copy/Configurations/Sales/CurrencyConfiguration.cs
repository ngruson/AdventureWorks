using AW.Domain.Sales;
using System.Data.Entity.ModelConfiguration;

namespace AW.Persistence.EntityFramework.Configurations.Sales
{
    public class CurrencyConfiguration : EntityTypeConfiguration<Currency>
    {
        public CurrencyConfiguration()
        {
            ToTable("Sales.Currency");
            HasKey(c => c.CurrencyCode);

            Property(c => c.CurrencyCode)
                .HasMaxLength(3);

            Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}