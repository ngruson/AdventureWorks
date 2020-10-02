using AW.Domain.Sales;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AW.Persistence.EntityFramework.Configurations.Sales
{
    public class SalesPersonConfiguration : EntityTypeConfiguration<SalesPerson>
    {
        public SalesPersonConfiguration()
        {
            ToTable("Sales.SalesPerson");
            HasKey(sp => sp.Id);

            Property(sp => sp.Id)
                .HasColumnName("BusinessEntityID")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(c => c.SalesTerritoryID)
              .HasColumnName("TerritoryID");

            Property(sp => sp.SalesQuota)
                .HasPrecision(19, 4)
                .HasColumnType("money");

            Property(sp => sp.Bonus)
                .HasPrecision(19, 4)
                .HasColumnType("money");

            Property(sp => sp.CommissionPct)
                .HasPrecision(10, 4);

            Property(sp => sp.SalesYTD)
                 .HasPrecision(19, 4)
                .HasColumnType("money");

            Property(sp => sp.SalesLastYear)
                .HasPrecision(19, 4)
                .HasColumnType("money");

            HasMany(e => e.SalesOrders)
                .WithOptional(e => e.SalesPerson)
                .HasForeignKey(e => e.SalesPersonID);

            HasMany(e => e.SalesPersonQuotaHistory);

            HasMany(e => e.SalesTerritoryHistory)
                .WithRequired(e => e.SalesPerson)
                .WillCascadeOnDelete(false);

            HasMany(e => e.Stores)
                .WithOptional(e => e.SalesPerson)
                .HasForeignKey(e => e.SalesPersonID);
        }
    }
}