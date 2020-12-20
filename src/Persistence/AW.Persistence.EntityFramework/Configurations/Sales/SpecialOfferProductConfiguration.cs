using AW.Core.Domain.Sales;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AW.Persistence.EntityFramework.Configurations.Sales
{
    public class SpecialOfferProductConfiguration : EntityTypeConfiguration<SpecialOfferProduct>
    {
        public SpecialOfferProductConfiguration()
        {
            ToTable("Sales.SpecialOfferProduct");
            HasKey(sop => new { sop.SpecialOfferID, sop.ProductID });

            Property(sop => sop.SpecialOfferID)
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(sop => sop.ProductID)
                .HasColumnOrder(1)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        }
    }
}