using AW.Services.Customer.Core.Entities;
using System.Data.Entity.ModelConfiguration;

namespace AW.Services.Customer.Infrastructure.EF6.Configurations
{
    public class SalesOrderConfiguration : EntityTypeConfiguration<SalesOrder>
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