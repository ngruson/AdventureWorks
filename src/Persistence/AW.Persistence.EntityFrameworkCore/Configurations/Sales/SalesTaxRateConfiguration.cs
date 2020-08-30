using AW.Domain.Sales;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Persistence.EntityFrameworkCore.Configurations.Sales
{
    public class SalesTaxRateConfiguration : IEntityTypeConfiguration<SalesTaxRate>
    {
        public void Configure(EntityTypeBuilder<SalesTaxRate> builder)
        {
            builder.ToTable("SalesTaxRate", "Sales");

            builder.Property(str => str.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(str => str.TaxRate)
                .HasColumnType("decimal(10,4)");
        }
    }
}