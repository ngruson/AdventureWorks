using System.Data.Entity.ModelConfiguration;

namespace AW.Services.SalesOrder.Infrastructure.EF6.Configurations
{
    public class SalesOrderConfiguration : EntityTypeConfiguration<Core.Entities.SalesOrder>
    {
        public SalesOrderConfiguration()
        {
            ToTable("SalesOrder");
            HasKey(p => p.Id);
            Property(p => p.Id)
                .HasColumnName("SalesOrderID");

            Ignore(p => p.SubTotal);
            Ignore(p => p.TaxAmt);
            Ignore(p => p.Freight);
            Ignore(p => p.TotalDue);
        }
    }
}