using AW.Domain.Sales;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AW.Persistence.EntityFramework.Configurations.Sales
{
    public class SalesPersonQuotaHistoryConfiguration : EntityTypeConfiguration<SalesPersonQuotaHistory>
    {
        public SalesPersonQuotaHistoryConfiguration()
        {
            ToTable("Sales.SalesPersonQuotaHistory");
            HasKey(spqh => new { spqh.Id, spqh.QuotaDate });

            Property(spqh => spqh.Id)
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(spqh => spqh.QuotaDate)
                .HasColumnOrder(1);

            Property(spqh => spqh.SalesQuota)
                .HasPrecision(19, 4)
                .HasColumnType("money");
        }
    }
}