using AW.Domain.Sales;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Persistence.EntityFrameworkCore.Configurations.Sales
{
    public class SalesTerritoryConfiguration : IEntityTypeConfiguration<SalesTerritory>
    {
        public void Configure(EntityTypeBuilder<SalesTerritory> builder)
        {
            builder.ToTable("SalesTerritory", "Sales");
            builder.HasKey(st => st.Id);

            builder.Property(st => st.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(st => st.CountryRegionCode)
                .IsRequired()
                .HasMaxLength(3);

            builder.Property(st => st.Group)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(st => st.SalesYTD)
                .HasColumnType("money");

            builder.Property(st => st.SalesLastYear)
                .HasColumnType("money");

            builder.Property(st => st.CostYTD)
                .HasColumnType("money");

            builder.Property(st => st.CostLastYear)
                .HasColumnType("money");

            builder.HasMany(e => e.StateProvinces)
                .WithOne(e => e.SalesTerritory)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(e => e.SalesTerritoryHistory)
                .WithOne(e => e.SalesTerritory)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}