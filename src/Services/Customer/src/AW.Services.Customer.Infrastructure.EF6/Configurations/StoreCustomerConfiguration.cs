using AW.Services.Customer.Core.Entities;
using System.Data.Entity.ModelConfiguration;

namespace AW.Services.Customer.Infrastructure.EF6.Configurations
{
    public class StoreCustomerConfiguration : EntityTypeConfiguration<StoreCustomer>
    {
        public StoreCustomerConfiguration()
        {
            ToTable("StoreCustomer");
            HasKey(p => p.Id);
        }
    }
}