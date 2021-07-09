using AW.Services.Customer.Core.Entities;
using System.Data.Entity.ModelConfiguration;

namespace AW.Services.Customer.Infrastructure.EF6.Configurations
{
    public class StoreCustomerContactConfiguration : EntityTypeConfiguration<StoreCustomerContact>
    {
        public StoreCustomerContactConfiguration()
        {
            ToTable("StoreCustomerContact");
            HasKey(p => p.Id);
        }
    }
}