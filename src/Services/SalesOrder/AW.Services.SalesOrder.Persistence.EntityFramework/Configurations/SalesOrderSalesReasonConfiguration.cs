using System.Data.Entity.ModelConfiguration;

namespace AW.Services.SalesOrder.Persistence.EntityFramework.Configurations
{
    public class SalesOrderSalesReasonConfiguration : EntityTypeConfiguration<Domain.SalesOrderSalesReason>
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