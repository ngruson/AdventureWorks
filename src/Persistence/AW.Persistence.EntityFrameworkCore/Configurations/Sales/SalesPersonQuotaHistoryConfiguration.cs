using AW.Domain.Sales;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Persistence.EntityFrameworkCore.Configurations.Sales
{
    public class SalesPersonQuotaHistoryConfiguration : IEntityTypeConfiguration<SalesPersonQuotaHistory>
    {
        public void Configure(EntityTypeBuilder<SalesPersonQuotaHistory> builder)
        {
            builder.ToTable("SalesPersonQuotaHistory", "Sales");
            builder.HasKey(spqh => new { spqh.Id, spqh.QuotaDate });

            builder.Property(spqh => spqh.Id)
                .ValueGeneratedNever();

            builder.Property(spqh => spqh.SalesQuota)
                .HasColumnType("money");
        }
    }
}