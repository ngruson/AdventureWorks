using AW.Services.SalesOrder.Core.Entities;
using System.Data.Entity.ModelConfiguration;

namespace AW.Services.SalesOrder.Infrastructure.EF6.Configurations
{
    public class SalesOrderSalesReasonConfiguration : EntityTypeConfiguration<SalesOrderSalesReason>
    {
        public SalesOrderSalesReasonConfiguration()
        {
            ToTable("SalesOrderSalesReason");
            HasKey(p => p.Id);
            Property(s => s.Id)
                .HasColumnName("SalesOrderSalesReasonID");
        }
    }
}