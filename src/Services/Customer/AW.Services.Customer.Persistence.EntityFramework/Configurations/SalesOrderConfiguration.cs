using System.Data.Entity.ModelConfiguration;

namespace AW.Services.Customer.Persistence.EntityFramework.Configurations
{
    public class SalesOrderConfiguration : EntityTypeConfiguration<Domain.SalesOrder>
    {
        public SalesOrderConfiguration()
        {
            ToTable("SalesOrder");
            HasKey(p => p.Id);

            Property(p => p.Id)
                .HasColumnName("SalesOrderID");
        }
    }
}