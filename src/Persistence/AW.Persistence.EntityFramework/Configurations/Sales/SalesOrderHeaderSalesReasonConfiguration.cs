using AW.Domain.Sales;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AW.Persistence.EntityFramework.Configurations.Sales
{
    public class SalesOrderHeaderSalesReasonConfiguration : EntityTypeConfiguration<SalesOrderHeaderSalesReason>
    {
        public SalesOrderHeaderSalesReasonConfiguration()
        {
            ToTable("Sales.SalesOrderHeaderSalesReason");
            HasKey(sohsr => new { sohsr.Id, sohsr.SalesReasonID });

            Property(sohsr => sohsr.Id)
                .HasColumnName("SalesOrderID")
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(sohsr => sohsr.SalesReasonID)
                .HasColumnOrder(1)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        }
    }
}