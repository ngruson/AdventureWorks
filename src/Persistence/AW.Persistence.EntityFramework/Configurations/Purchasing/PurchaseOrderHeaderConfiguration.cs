using AW.Domain.Purchasing;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AW.Persistence.EntityFramework.Configurations.Purchasing
{
    public class PurchaseOrderHeaderConfiguration : EntityTypeConfiguration<PurchaseOrderHeader>
    {
        public PurchaseOrderHeaderConfiguration()
        {
            ToTable("Purchasing.PurchaseOrderHeader");
            HasKey(poh => poh.PurchaseOrderID);

            Property(poh => poh.SubTotal)
                .HasPrecision(19, 4)
                .HasColumnType("money");
            
            Property(poh => poh.TaxAmt)
                .HasPrecision(19, 4)
                .HasColumnType("money");

            Property(poh => poh.Freight)
                .HasPrecision(19, 4)
                .HasColumnType("money");

            Property(poh => poh.TotalDue)
                .HasPrecision(19, 4)
                .HasColumnType("money")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);

            HasMany(e => e.PurchaseOrderDetail);
        }
    }
}