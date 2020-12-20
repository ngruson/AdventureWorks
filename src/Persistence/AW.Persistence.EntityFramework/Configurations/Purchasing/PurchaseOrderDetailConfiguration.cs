using AW.Core.Domain.Purchasing;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AW.Persistence.EntityFramework.Configurations.Purchasing
{
    public class PurchaseOrderDetailConfiguration : EntityTypeConfiguration<PurchaseOrderDetail>
    {
        public PurchaseOrderDetailConfiguration()
        {
            ToTable("Purchasing.PurchaseOrderDetail");
            HasKey(pod => new { pod.PurchaseOrderID, pod.PurchaseOrderDetailID });

            Property(pod => pod.PurchaseOrderID)
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(pod => pod.PurchaseOrderDetailID)
                .HasColumnOrder(1);

            Property(pod => pod.UnitPrice)
                .HasPrecision(19, 4)
                .HasColumnType("money");

            Property(pod => pod.LineTotal)
                .HasPrecision(19, 4)
                .HasColumnType("money")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);

            Property(pod => pod.ReceivedQty)
                .HasPrecision(8, 2);

            Property(pod => pod.RejectedQty)
                .HasPrecision(8, 2);

            Property(pod => pod.StockedQty)
                .HasPrecision(9, 2)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
        }
    }
}