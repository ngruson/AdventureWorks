using AW.Domain.Sales;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AW.Persistence.EntityFramework.Configurations.Sales
{
    public class SalesTerritoryHistoryConfiguration : EntityTypeConfiguration<SalesTerritoryHistory>
    {
        public SalesTerritoryHistoryConfiguration()
        {
            ToTable("Sales.SalesTerritoryHistory");
            HasKey(sth => new { sth.BusinessEntityID, sth.TerritoryID, sth.StartDate });

            Property(sth => sth.BusinessEntityID)
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(sth => sth.TerritoryID)
                .HasColumnOrder(1)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(sth => sth.StartDate)
                .HasColumnOrder(2);
        }
    }
}