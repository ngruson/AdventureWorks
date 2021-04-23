using System.Data.Entity.ModelConfiguration;

namespace AW.Services.SalesOrder.Persistence.EntityFramework.Configurations
{
    public class CustomerAddressConfiguration : EntityTypeConfiguration<Domain.CustomerAddress>
    {
        public CustomerAddressConfiguration()
        {
            ToTable("CustomerAddress");
            HasKey(p => p.Id);
        }
    }
}