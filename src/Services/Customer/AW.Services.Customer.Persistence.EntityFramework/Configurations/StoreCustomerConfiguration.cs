using System.Data.Entity.ModelConfiguration;

namespace AW.Services.Customer.Persistence.EntityFramework.Configurations
{
    public class StoreCustomerConfiguration : EntityTypeConfiguration<Domain.StoreCustomer>
    {
        public StoreCustomerConfiguration()
        {
            ToTable("StoreCustomer");
            HasKey(p => p.Id);
        }
    }
}