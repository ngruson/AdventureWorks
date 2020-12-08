using AW.Domain.Sales;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AW.Persistence.EntityFramework.Configurations.Sales
{
    public class SalesOrderDetailConfiguration : EntityTypeConfiguration<SalesOrderDetail>
    {
        public SalesOrderDetailConfiguration()
        {
            ToTable("Sales.SalesOrderDetail");
            HasKey(sod => new { sod.SalesOrderID, sod.SalesOrderDetailID });

            Property(sod => sod.SalesOrderID)
                .HasColumnName("SalesOrderID")
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(sod => sod.SalesOrderDetailID)
                .HasColumnOrder(1);

            Property(sod => sod.CarrierTrackingNumber)
                .HasMaxLength(25);

            Property(sod => sod.UnitPrice)
                .HasPrecision(19, 4)
                .HasColumnType("money");

            Property(sod => sod.UnitPriceDiscount)
                .HasPrecision(19, 4)
                .HasColumnType("money");

            Property(sod => sod.LineTotal)
                .HasPrecision(38, 6)
                .HasColumnType("numeric")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
        }
    }
}