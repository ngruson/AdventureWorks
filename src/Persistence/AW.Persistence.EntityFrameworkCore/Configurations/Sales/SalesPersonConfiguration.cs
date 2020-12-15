using AW.Domain.Sales;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Persistence.EntityFrameworkCore.Configurations.Sales
{
    public class SalesPersonConfiguration : IEntityTypeConfiguration<SalesPerson>
    {
        public void Configure(EntityTypeBuilder<SalesPerson> builder)
        {
            builder.ToTable("SalesPerson", "Sales");
            builder.HasKey(sp => sp.Id);

            builder.Property(sp => sp.SalesQuota)
                .HasColumnType("money");

            builder.Property(sp => sp.Bonus)
                .HasColumnType("money");

            builder.Property(sp => sp.CommissionPct)
                .HasColumnType("decimal(10,4)");

            builder.Property(sp => sp.SalesYTD)
                .HasColumnType("money");

            builder.Property(sp => sp.SalesLastYear)
                .HasColumnType("money");

            builder.HasMany(e => e.SalesOrders)
                .WithOne(e => e.SalesPerson)
                .HasForeignKey(e => e.SalesPersonID);

            builder.HasMany(e => e.SalesPersonQuotaHistory);

            builder.HasMany(e => e.SalesTerritoryHistory)
                .WithOne(e => e.SalesPerson)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(e => e.Stores)
                .WithOne(e => e.SalesPerson)
                .HasForeignKey(e => e.SalesPersonID);
        }
    }
}