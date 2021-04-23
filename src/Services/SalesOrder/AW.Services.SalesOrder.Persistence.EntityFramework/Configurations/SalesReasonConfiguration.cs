using System.Data.Entity.ModelConfiguration;

namespace AW.Services.SalesOrder.Persistence.EntityFramework.Configurations
{
    public class SalesReasonConfiguration : EntityTypeConfiguration<Domain.SalesReason>
    {
        public SalesReasonConfiguration()
        {
            ToTable("SalesReason");
            HasKey(p => p.Id);
            Property(s => s.Id)
                .HasColumnName("SalesReasonID");
        }
    }
}