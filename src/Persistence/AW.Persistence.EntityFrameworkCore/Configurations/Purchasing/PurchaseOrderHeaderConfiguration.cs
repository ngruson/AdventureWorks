using AW.Core.Domain.Purchasing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Persistence.EntityFrameworkCore.Configurations.Purchasing
{
    public class PurchaseOrderHeaderConfiguration : IEntityTypeConfiguration<PurchaseOrderHeader>
    {
        public void Configure(EntityTypeBuilder<PurchaseOrderHeader> builder)
        {
            builder.ToTable("Purchasing.PurchaseOrderHeader");
            builder.HasKey(poh => poh.PurchaseOrderID);

            builder.Property(poh => poh.SubTotal)
                .HasColumnType("money");

            builder.Property(poh => poh.TaxAmt)
                .HasColumnType("money");

            builder.Property(poh => poh.Freight)
                .HasColumnType("money");

            builder.Property(poh => poh.TotalDue)
                .HasColumnType("money")
                .ValueGeneratedOnAddOrUpdate();

            builder.HasMany(e => e.PurchaseOrderDetail);
        }
    }
}