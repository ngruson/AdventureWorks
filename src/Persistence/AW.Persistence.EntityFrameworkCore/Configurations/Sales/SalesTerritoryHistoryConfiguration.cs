using AW.Domain.Sales;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Persistence.EntityFrameworkCore.Configurations.Sales
{
    public class SalesTerritoryHistoryConfiguration : IEntityTypeConfiguration<SalesTerritoryHistory>
    {
        public void Configure(EntityTypeBuilder<SalesTerritoryHistory> builder)
        {
            builder.ToTable("SalesTerritoryHistory", "Sales");
            builder.HasKey(sth => new { sth.Id, sth.TerritoryID, sth.StartDate });

            builder.Property(sth => sth.Id)
                .ValueGeneratedNever();

            builder.Property(sth => sth.TerritoryID)
                .ValueGeneratedNever();
        }
    }
}