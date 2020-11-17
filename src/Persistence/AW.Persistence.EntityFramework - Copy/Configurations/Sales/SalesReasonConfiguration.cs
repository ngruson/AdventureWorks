using AW.Domain.Sales;
using System.Data.Entity.ModelConfiguration;

namespace AW.Persistence.EntityFramework.Configurations.Sales
{
    public class SalesReasonConfiguration : EntityTypeConfiguration<SalesReason>
    {
        public SalesReasonConfiguration()
        {
            ToTable("Sales.SalesReason");

            Property(sr => sr.Name)
                .IsRequired()
                .HasMaxLength(50);

            Property(sr => sr.ReasonType)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}