using AW.Core.Domain.Sales;
using System.Data.Entity.ModelConfiguration;

namespace AW.Persistence.EntityFramework.Configurations.Sales
{
    public class SalesTaxRateConfiguration : EntityTypeConfiguration<SalesTaxRate>
    {
        public SalesTaxRateConfiguration()
        {
            ToTable("Sales.SalesTaxRate");

            Property(str => str.Name)
                .IsRequired()
                .HasMaxLength(50);

            Property(str => str.TaxRate)
                .HasPrecision(10, 4);
        }
    }
}