using AW.Services.Customer.Core.Entities;
using System.Data.Entity.ModelConfiguration;

namespace AW.Services.Customer.Infrastructure.EF6.Configurations
{
    public class CustomerAddressConfiguration : EntityTypeConfiguration<CustomerAddress>
    {
        public CustomerAddressConfiguration()
        {
            ToTable("CustomerAddress");
            HasKey(p => p.Id);

            Property(c => c.Id)
                .HasColumnName("CustomerAddressID");
        }
    }
}