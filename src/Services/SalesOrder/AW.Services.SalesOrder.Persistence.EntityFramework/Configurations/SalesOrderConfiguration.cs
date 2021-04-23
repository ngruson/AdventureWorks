using System.Data.Entity.ModelConfiguration;

namespace AW.Services.SalesOrder.Persistence.EntityFramework.Configurations
{
    public class SalesOrderConfiguration : EntityTypeConfiguration<Domain.SalesOrder>
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