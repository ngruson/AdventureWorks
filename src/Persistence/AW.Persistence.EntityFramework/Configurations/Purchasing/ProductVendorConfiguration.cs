using AW.Core.Domain.Purchasing;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AW.Persistence.EntityFramework.Configurations.Purchasing
{
    public class ProductVendorConfiguration : EntityTypeConfiguration<ProductVendor>
    {
        public ProductVendorConfiguration()
        {
            ToTable("Purchasing.ProductVendor");
            HasKey(pv => new { pv.ProductID, pv.BusinessEntityID });

            Property(pv => pv.ProductID)
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(pv => pv.BusinessEntityID)
                .HasColumnOrder(1)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(pv => pv.StandardPrice)
                .HasPrecision(19, 4)
                .HasColumnType("money");

            Property(pv => pv.LastReceiptCost)
                .HasPrecision(19, 4)
                .HasColumnType("money");

            Property(pv => pv.UnitMeasureCode)
                .IsRequired()
                .HasMaxLength(3);
        }
    }
}