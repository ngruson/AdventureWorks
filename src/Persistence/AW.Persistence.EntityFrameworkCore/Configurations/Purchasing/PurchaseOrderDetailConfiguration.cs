using AW.Core.Domain.Purchasing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Persistence.EntityFrameworkCore.Configurations.Purchasing
{
    public class PurchaseOrderDetailConfiguration : IEntityTypeConfiguration<PurchaseOrderDetail>
    {
        public void Configure(EntityTypeBuilder<PurchaseOrderDetail> builder)
        {
            builder.ToTable("Purchasing.PurchaseOrderDetail");
            builder.HasKey(pod => new { pod.PurchaseOrderID, pod.PurchaseOrderDetailID });

            builder.Property(pod => pod.PurchaseOrderID)
                .ValueGeneratedNever();

            builder.Property(pod => pod.UnitPrice)
                .HasColumnType("money");

            builder.Property(pod => pod.LineTotal)
                .HasColumnType("money")
                .ValueGeneratedOnAddOrUpdate();

            builder.Property(pod => pod.ReceivedQty)
                .HasColumnType("decimal(8,2)");

            builder.Property(pod => pod.RejectedQty)
                .HasColumnType("decimal(8,2)");

            builder.Property(pod => pod.StockedQty)
                .HasColumnType("decimal(9,2)")
                .ValueGeneratedOnAddOrUpdate();
        }
    }
}