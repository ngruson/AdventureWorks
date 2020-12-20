using AW.Core.Domain.Sales;
using System.Data.Entity.ModelConfiguration;

namespace AW.Persistence.EntityFramework.Configurations.Sales
{
    public class SalesReasonConfiguration : EntityTypeConfiguration<SalesReason>
    {
        public SalesReasonConfiguration()
        {
            ToTable("Sales.SalesReason");
            HasKey(sr => sr.Id);

            Property(sr => sr.Id)
                .HasColumnName("SalesReasonID");

            Property(sr => sr.Name)
                .IsRequired()
                .HasMaxLength(50);

            Property(sr => sr.ReasonType)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}