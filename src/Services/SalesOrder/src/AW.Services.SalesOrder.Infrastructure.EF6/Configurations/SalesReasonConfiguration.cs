using AW.Services.SalesOrder.Core.Entities;
using System.Data.Entity.ModelConfiguration;

namespace AW.Services.SalesOrder.Infrastructure.EF6.Configurations
{
    public class SalesReasonConfiguration : EntityTypeConfiguration<SalesReason>
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