using AW.Core.Domain.HumanResources;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AW.Persistence.EntityFramework.Configurations.HumanResources
{
    public class EmployeePayHistoryConfiguration : EntityTypeConfiguration<EmployeePayHistory>
    {
        public EmployeePayHistoryConfiguration()
        {
            ToTable("HumanResources.EmployeePayHistory");
            HasKey(eph => new { eph.BusinessEntityID, eph.RateChangeDate });

            Property(eph => eph.BusinessEntityID)
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(eph => eph.RateChangeDate)
                .HasColumnOrder(1);

            Property(eph => eph.Rate)
                .HasColumnType("money");
        }
    }
}