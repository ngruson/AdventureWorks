using System.Data.Entity.ModelConfiguration;

namespace AW.Services.Customer.Persistence.EntityFramework.Configurations
{
    public class StoreCustomerContactConfiguration : EntityTypeConfiguration<Domain.StoreCustomerContact>
    {
        public StoreCustomerContactConfiguration()
        {
            ToTable("StoreCustomerContact");
            HasKey(p => p.Id);
        }
    }
}